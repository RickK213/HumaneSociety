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
            database.AddAnimal(animal);
            //4. submit all necessary information to the database
        }

        public override void StartFlow()
        {
            //Main menu
            string userInput = UI.GetMainMenuOptions(role);
            
            switch (userInput)
            {
                case ("1"):
                    AddAnimalToDatabase();
                    break;
                case ("2"):
                    SearchAnimals();
                    break;
                case ("3"):
                    ListAnimals();
                    break;
                case ("4"):
                    SearchAdopters();
                    break;
                case ("5"):
                    ListAdopters();
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
            var dataTable = database.GetAllValues("*", "Adopters").AsEnumerable().ToList();
            //var scanDataTable = dataTable.ForEach(
            //    m 

                
            //    )
            foreach (var item in dataTable)
            {
                Console.WriteLine(item);
            }
        }

        void ListAdopters()
        {

        }

        public void SearchByAdoptionStatus()
        {
            //if userInput == 0 true, 1 false; = true/false
            animal.IsAdopted = false;
        }
        public override void SearchAnimals()
        {
            //turn true if user enters a search option
            bool isSearchingByName = false;
            bool isSearchingBySpecies = false;
            bool isSearchingByImmunization = true;
            bool isSearchingByPrice = false;
            bool isSearchingByAdoptionStatus = true;
            //add method to check Adopter's HasPaid status
            bool isSearchingByPaymentStatus = false;
            //

            //user specified search variables
            string nameToSearch = "Oreo";   //do .ToLower() later on to keep it lower case
            string speciesToSearch = "dog";
            bool statusOfImmunization = true;
            double priceAmountToSearch = 113.09;
            bool animalIsAdopted = false;
            //add method to check Adopter's HasPaid status
            bool adopterHasPayed = false;
            //
            List<Animal> animals = database.SearchAnimals();
            var searchedAnimals = animals.Where(
                m =>
                (isSearchingByName ? m.Name == nameToSearch : m.Name != null) &&
                (isSearchingBySpecies ? m.Species == speciesToSearch : m.Species != null) &&
                (isSearchingByImmunization ? m.IsImmunized == statusOfImmunization : m.IsImmunized != null) &&
                (isSearchingByPrice ? m.Price < priceAmountToSearch : m.Price > 0) &&
                (isSearchingByAdoptionStatus ? m.IsAdopted == animalIsAdopted : m.IsAdopted != null)
                ).OrderBy(m => m.AnimalID);
            foreach (Animal animal in searchedAnimals)
            {
                Console.WriteLine("(Animal ID: " + animal.AnimalID + ") " + animal.Name + " was found using the search criteria entered.");
            }
        }
        public void SetAnimalRoomNumber(bool allowNone)
        {
            userInput = UI.GetAnimalRoomNumber(allowNone);
            animal.RoomNumber = Convert.ToInt32(userInput);
        }
    }
}
