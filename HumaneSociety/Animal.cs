using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    abstract class Animal
    {
        private string species;
        private string name;
        private bool isAdopted;
        private bool isImmunized;
        private double price;

        public string Species
        {
            get
            {
                return species;
            }
            set
            {
                if (species == null)
                {
                    species = value;
                }
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public bool IsAdopted
        {
            get
            {
                return isAdopted;
            }
            set
            {
                isAdopted = value;
            }
        }
        public bool IsImmunized
        {
            get
            {
                return isImmunized;
            }
            set
            {
                isImmunized = value;
            }
        }
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public Animal()//string name, bool isAdopted, bool isImmunized, double price
        {

        }
    }
}
