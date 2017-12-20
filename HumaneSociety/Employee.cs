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
            //SetAnimalFoodPerWeek(false);
            //SetAnimalPrice(false);
            animal.IsAdopted = false;
            
            //1. determine species - done
            //2. ask for name of animal, room number, if it is immunized, food per week and price adopter will pay
            //3. isAdopted = false by default
            //4. submit all necessary information to the database
        }

        public override void StartFlow()
        {
            //Main menu
            string userInput = UI.GetMainMenuOptions(role);
            
            switch (userInput)
            {
                case ("0"):
                    AddAnimalToDatabase();
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
