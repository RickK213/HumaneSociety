using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public abstract class Animal
    {
        private string species;
        private int speciesID = 0;
        private string name;
        private bool isAdopted;
        private bool isImmunized;
        private int roomNumber;
        private int ounceFoodPerWeek;
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
        public int SpeciesID
        {
            get
            {
                return speciesID;
            }
            set
            {
                if(speciesID == 0)
                {
                    speciesID = value;
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
        public int RoomNumber
        {
            get
            {
                return roomNumber;
            }
            set
            {
                roomNumber = value;
            }
        }
        public int OunceFoodPerWeek
        {
            get
            {
                return ounceFoodPerWeek;
            }
            set
            {
                ounceFoodPerWeek = value;
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
