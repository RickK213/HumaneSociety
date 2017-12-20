using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public class Employee : User
    {
        AnimalFactory factory = new ConcreteAnimalFactory();
        Animal animal = null;
        //Animal = factory. 
        public Employee()
        {
            role = "employee";
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
