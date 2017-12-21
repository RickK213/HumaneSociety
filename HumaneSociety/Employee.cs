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

        public void AddAnimalToDatabase()
        {
            SetAnimal(false, "Add an animal");    //override method if necessary
            SetAnimalName(false);
            SetAnimalRoomNumber(false);
            SetAnimalImmunizationStatus(false);
            SetAnimalFoodPerWeek(false);
            SetAnimalPrice(false);
            animal.IsAdopted = false;
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
                case ("q"):
                    Environment.Exit(-1);
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
            //if userInput == 0 true, 1 false; = true/false
            animal.IsAdopted = false;
        }
        public void SetAnimalRoomNumber(bool allowNone)
        {
            userInput = UI.GetAnimalRoomNumber(allowNone);
            animal.RoomNumber = Convert.ToInt32(userInput);
        }
    }
}
