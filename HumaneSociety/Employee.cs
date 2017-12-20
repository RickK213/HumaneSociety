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
            
        }
        //for AddAnimal
        //1. determine species
        //2. ask for name of animal, if it is immunized, food per week and price adopter will pay
        //3. isAdopted = false by default
        //4. submit all necessary information to the database
        public override void StartFlow()
        {
            base.StartFlow();
        }
        public void SearchByAdoptionStatus()
        {
            //if userInput == 0 true, 1 false; = true/false
            species.IsAdopted = false;
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
