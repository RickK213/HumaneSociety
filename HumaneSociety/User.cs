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
        public Animal species = null;
        string userInput;
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
            //userInput get name of animal
            species.Name = userInput;
        }
        public virtual void SearchByAnimalSpecies()
        {
            //userInput get species of animal, this should be determined first if possible
            species = determineSpecies.IdentifyAnimal(userInput);
        }
        public void SearchByImmunizationStatus()
        {
            //if userInput == 0 true, 1 false; = true/false
            species.IsImmunized = true;
        }
        public void SearchByPrice()
        {
            //verify userInput is a money number before converting
            species.Price = Convert.ToDouble(userInput);
        }
        public void ScanDatabase()
        {
            try
            {
                //if(species.Species != null)
                //add to database scan species.species
                //if(species.Name != null)
                //add to database scan species.Name;
                //if(species.IsAdopted != null)
                //add to database scan species.IsAdopted;
                //if(species.IsImmunized != null)
                //add to database scan species.IsImmunized;
                //if(species.Price != null)
                //add to database scan species.Price;
                //inform user of searched attributes
                //show user results from database if any
            }
            catch
            {

            }
        }
    }
}
