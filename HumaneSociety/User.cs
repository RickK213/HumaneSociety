using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public abstract class User
    {
        AnimalFactory determineSpecies = new ConcreteAnimalFactory();
        Animal species = null;
        public string role;
        public User()
        {

        }
        public virtual void StartFlow()
        {
            //do stuff that adopter sees
        }
        public virtual void SearchByAnimalName()
        {

        }
        public virtual void SearchByAnimalSpecies()
        {

        }
        public void SearchByImmunizationStatus()
        {

        }
        public void SearchByPrice()
        {

        }
        public void SearchByKeyWord()
        {

        }

        public void SearchAnimals()
        {

        }

        public void ListAnimals()
        {

        }
    }
}
