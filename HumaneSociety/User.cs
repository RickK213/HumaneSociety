using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public abstract class User
    {
        public AnimalFactory animalFactory = new ConcreteAnimalFactory();
        public Animal animal = null;
        string userInput;
        public string role;
        public User()
        {

        }
        public virtual void StartFlow()
        {
            //do stuff that adopter sees
        }
        public void SetAnimalName()
        {
            //userInput get name of animal
            animal.Name = userInput;
        }
        public virtual void SetAnimal(bool allowAll, string pageHeader)
        {
            UI.DisplayPageHeader(pageHeader);
            string userInput = UI.GetAddAnimalOption(allowAll);
            switch (userInput)
            {
                case ("0"):
                    animal = animalFactory.CreateAnimal("dog");
                    break;
                case ("1"):
                    animal = animalFactory.CreateAnimal("cat");
                    break;
                case ("2"):
                    animal = animalFactory.CreateAnimal("bird");
                    break;
                case ("3"):
                    animal = animalFactory.CreateAnimal("rabbit");
                    break;
                case ("4"):
                    animal = animalFactory.CreateAnimal("ferret");
                    break;
                case ("5"):
                    break;
            }
        }
        public void SetAnimalImmunizationStatus()
        {
            //if userInput == 0 true, 1 false; = true/false
            animal.IsImmunized = true;
        }
        public void SetAnimalPrice()
        {
            //verify userInput is a money number before converting
            animal.Price = Convert.ToDouble(userInput);
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
        public void SetAnimalFoodPerWeek()
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
