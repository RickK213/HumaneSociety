using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public abstract class AnimalFactory
    {
        public abstract Animal CreateAnimal(string userInput);
    }
}
