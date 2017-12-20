using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public class Animal
    {
        private string name;
        private bool isAdopted;
        private bool isImmunized;
        private double price;

        public Animal()
        {

        }
        public Animal IdentifyAnimal(string animalName)
        {
            switch (animalName.ToLower())
            {
                case "dog":
                    return new Dog();
                case "cat":
                    return new Cat();
                case "bird":
                    return new Bird();
                case "ferret":
                    return new Ferret();
                case "rabbit":
                    return new Rabbit();
                default:
                    throw new ApplicationException(string.Format("We don't have {0} at our facility.  Please enter a vaild choice.", animalName));
            }
        }
    }
}
