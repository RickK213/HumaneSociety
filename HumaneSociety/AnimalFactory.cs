using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    abstract class AnimalFactory
    {
        public abstract Animal IdentifyAnimal(string userInput);
    }
}
