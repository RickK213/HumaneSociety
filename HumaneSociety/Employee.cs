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

        public Employee()
        {

            role = "employee";
            csvReader = new CSVReader();
        }

        public void AddAnimalToDatabase()
        {
            SetAnimal(false, "Add an animal");    //override method if necessary
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
            //Main menu
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
                    SearchAdopters();
                    StartFlow();
                    break;
                case ("5"):
                    ListAdopters();
                    StartFlow();
                    break;
                case ("6"):
                    csvReader.Start();
                    StartFlow();
                    break;
                case ("q"):
                    Environment.Exit(-1);
                    break;
            }

        }

        public void SearchAdopters()
        {
            List<User> adopters = database.RetrieveUsers();
            EditAdopterFromList(adopters);

            //send list of adopters to specified search and code to use: 
            //check hasPaid: var scan = adopters.Where(m => m.HasPaid == 0);
            //check if they adopted an animal: var scan = adopters.Where(m => m.AnimalAdopted > 0);
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
                UI.GetAnyKeyToContinue("No adopters in database. Press any key to return to main menu.");
                return;
            }
        }

        public void SearchByAdoptionStatus()
        {
            //if userInput == 0 true, 1 false; = true/false
            animal.IsAdopted = false;
        }
        public override void SearchAnimals()
        {
            userInput = UI.GetSearchOption(role);

            //bool adopterHasPayed;

            //menuOptions.Append("1: Search by Animal Name\n");
            //menuOptions.Append("2: Search by Species\n");
            //menuOptions.Append("3: Search by Immunization Status\n");
            //menuOptions.Append("4: Search by Price\n");
            //menuOptions.Append("5: Search by Multiple Criteria\n");
            //if (role == "employee")
            //{
            //    menuOptions.Append("6: Search by Adoption Status\n");
            //}
            //menuOptions.Append("m: Return to Main Menu\n");
            //menuOptions.Append("q: Quit Application");

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
