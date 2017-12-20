using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class ConcreteAnimalFactory : AnimalFactory
    {
        public override Animal CreateAnimal(string userInput)
        {
            switch (userInput.ToLower())
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
                case "null":
                    return null;
                default:
                    throw new ApplicationException(string.Format("We don't have {0} at our facility.  Please enter a vaild choice.", userInput));
            }
        }
    }
}
