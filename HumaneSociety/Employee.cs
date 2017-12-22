using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public class Employee : User
    {

        //member variables
        CSVReader csvReader;

        //constructor
        public Employee()
        {

            role = "employee";
            csvReader = new CSVReader();
        }

        //member methods
        public void AddAnimalToDatabase()
        {
            SetAnimal(false, "Add an animal");
            SetAnimalName(false);
            SetAnimalRoomNumber(false);
            SetAnimalImmunizationStatus(false);
            SetAnimalFoodPerWeek(false);
            SetAnimalPrice(false);
            animal.IsAdopted = false;
            bool isSuccessful = database.AddAnimal(animal);
            if (isSuccessful)
            {
                UI.GetAnyKeyToContinue("Animal added to database. Press any key to return to main menu.");
            }
            else
            {
                UI.GetAnyKeyToContinue("Failed to add animal, room number is already occupied.  Verify the room the animal is going into isn't occupied and try again.");
            }
        }

        public override void StartFlow()
        {
            string userInput = UI.GetMainMenuOptions(role);
            
            switch (userInput)
            {
                case ("1"):
                    AddAnimalToDatabase();
                    StartFlow();
                    break;
                case ("2"):
                    SearchAnimals();
                    StartFlow();
                    break;
                case ("3"):
                    ListAnimals();
                    StartFlow();
                    break;
                case ("4"):
                    ListAdopters();
                    StartFlow();
                    break;
                case ("5"):
                    csvReader.Start();
                    StartFlow();
                    break;
                case ("q"):
                    Environment.Exit(-1);
                    break;
            }

        }

        void ListAdopters()
        {
            UI.DisplayPageHeader("All Adopters");
            List<User> adopters = database.RetrieveUsers();
            if (adopters.Count > 0)
            {
                UI.DisplayList(adopters);
                EditAdopterFromList(adopters);
            }
            else
            {
                UI.GetAnyKeyToContinue("No adopters found. Press any key to return to main menu.");
                return;
            }
        }

        public override void SearchAnimals()
        {
            userInput = UI.GetSearchOption(role);

            switch (userInput)
            {
                case ("1"):
                    string nameToSearch = UI.GetAnimalName(false);
                    SearchByName(nameToSearch);
                    StartFlow();
                    break;
                case ("2"):
                    string speciesToSearch = UI.GetSpeciesName();
                    SearchBySpecies(speciesToSearch);
                    StartFlow();
                    break;
                case ("3"):
                    string immunizationToSearch = UI.GetImmunizationStatus(true);
                    SearchByImmunization(immunizationToSearch);
                    StartFlow();
                    break;
                case ("4"):
                    string priceMax = UI.GetAnimalPrice(true);
                    SearchByMaxPrice(priceMax);
                    StartFlow();
                    break;
                case ("5"):
                    List<Animal> foundAnimals = SearchByMultipleCriteria();
                    if (foundAnimals.Count > 0)
                    {
                        EditAnimalFromList(foundAnimals);
                    }
                    UI.DisplayNoAnimalsFound();
                    StartFlow();
                    break;
                case ("6"):
                    string adoptionToSearch = UI.GetAdoptionStatus();
                    SearchByAdoption(adoptionToSearch);
                    StartFlow();
                    break;
                case ("m"):
                    StartFlow();
                    break;
                case ("q"):
                    Environment.Exit(-1);
                    break;
            }

        }

        public void SetAnimalRoomNumber(bool allowNone)
        {
            userInput = UI.GetAnimalRoomNumber(allowNone);
            animal.RoomNumber = Convert.ToInt32(userInput);
        }
    }
}
