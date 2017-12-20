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
        public override void StartFlow()
        {
            base.StartFlow();
        }
        public override void SearchByAnimalName()
        {
            base.SearchByAnimalName();
        }
        public override void SearchByAnimalType()
        {
            base.SearchByAnimalType();
        }
    }
}
