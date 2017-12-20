using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public class Employee : User
    {
               
        public Employee()
        {
            role = "employee";
        }

        public void AddAnimal()
        {
            string userInput = UI.GetAddAnimalOption();
            Animal animalToAdd;
            switch (userInput)
            {
                case ("0"):
                    //create a dog object
                    break;
                case ("1"):
                    //create a cat object
                    break;
                case ("2"):
                    //create a bird object
                    break;
                case ("3"):
                    //create a rabbit object
                    break;
                case ("4"):
                    //create a ferret object
                    break;
            }
        }

        public override void StartFlow()
        {
            //Main menu
            string userInput = UI.GetMainMenuOptions(role);
            
            switch (userInput)
            {
                case ("0"):
                    AddAnimal();
                    break;
                case ("1"):
                    SearchAnimals();
                    break;
                case ("2"):
                    ListAnimals();
                    break;
                case ("3"):
                    SearchAdopters();
                    break;
                case ("4"):
                    ListAdopters();
                    break;
            }

        }

        void SearchAdopters()
        {

        }

        void ListAdopters()
        {

        }

        public void SearchByAdoptionStatus()
        {

        }

        public override void SearchByAnimalName()
        {
            base.SearchByAnimalName();
        }

        public override void SearchByAnimalSpecies()
        {
            base.SearchByAnimalSpecies();
        }
    }
}
