using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public abstract class User
    {
        //member variables
        public DatabaseControl database = new DatabaseControl();
        public AnimalFactory animalFactory = new ConcreteAnimalFactory();
        public Animal animal = null;
        public string userInput;
        public string role;
        string name;
        string email;
        string streetAddress;
        string city;
        string state;
        string zipCode;

        //properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string StreetAddress
        {
            get { return streetAddress; }
            set { streetAddress = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        //constructor
        public User()
        {

        }

        public virtual void StartFlow()
        {
            //do stuff that adopter sees
        }

        public void SetAnimalName(bool allowNone)
        {
            userInput = UI.GetAnimalName(allowNone);
            animal.Name = userInput;
        }
        public virtual void SetAnimal(bool allowAll, string pageHeader)
        {
            UI.DisplayPageHeader(pageHeader);
            string userInput = UI.GetAddAnimalOption(allowAll);
            switch (userInput)
            {
                case ("1"):
                    animal = animalFactory.CreateAnimal("dog");
                    break;
                case ("2"):
                    animal = animalFactory.CreateAnimal("cat");
                    break;
                case ("3"):
                    animal = animalFactory.CreateAnimal("bird");
                    break;
                case ("4"):
                    animal = animalFactory.CreateAnimal("rabbit");
                    break;
                case ("5"):
                    animal = animalFactory.CreateAnimal("ferret");
                    break;
                case ("6"):
                    animal = null;
                    break;
                case ("m"):
                    StartFlow();
                    break;
                case ("q"):
                    Environment.Exit(-1);
                    break;
            }
        }
        public void SetAnimalImmunizationStatus(bool allowNone)
        {
            userInput = UI.GetImmunizationStatus(allowNone);
            string immunizedStatus = userInput;
            if ( immunizedStatus == "y" )
            {
                animal.IsImmunized = true;
            }
            else
            {
                animal.IsImmunized = false;
            }
        }

        public void SetAnimalPrice(bool allowNone)
        {
            //verify userInput is a money number before converting
            userInput = UI.GetAnimalPrice(allowNone);
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
        public void SetAnimalFoodPerWeek(bool allowNone)
        {
            userInput = UI.GetFoodPerWeek(allowNone);
            animal.OunceFoodPerWeek = Convert.ToInt32(userInput);
        }

        public void SearchAnimals()
        {
            bool isSearchingByName = false;
            bool isSearchingBySpecies = false;
            bool isSearchingByImmunization = false;
            bool isSearchingByPrice = false;

            //user specified search variables
            string nameToSearch = "bluto";
            string speciesToSearch = "dog";
            bool statusOfImmunization = true;
            double priceAmountToSearch = 13.09;
            //bool isSearchingByAdoptionStatus = false;
            //bool isSearchingByPaymentStatus = false;
            List<Animal> animals = database.SearchAnimals();
            var immunizedAnimals = animals.Where(
                m => 
                (isSearchingByName ? m.Name == nameToSearch : m.Name != null) &&
                (isSearchingBySpecies ? m.Species == speciesToSearch : m.Species != null) &&
                (isSearchingByImmunization ? m.IsImmunized == statusOfImmunization : m.IsImmunized != null) &&
                (isSearchingByPrice ? m.Price < priceAmountToSearch : m.Price > 0)
                ).OrderBy(m => m.AnimalID);
            foreach (Animal animal in immunizedAnimals)
            {
                Console.WriteLine("(Animal ID: " + animal.AnimalID + ") " + animal.Name + (animal.IsImmunized ? " is immunized" : " is not immunized"));
            }
        }

        public void ListAnimals()
        {
            List<Animal> animals = database.SearchAnimals();
            var immunizedAnimals = animals.Where(m => m.IsImmunized == true).OrderBy(m => m.AnimalID);
            foreach (Animal animal in immunizedAnimals)
            {
                Console.WriteLine("(Animal ID: " + animal.AnimalID + ") " + animal.Name + (animal.IsImmunized ? " is immunized" : " is not immunized"));
            }
            //ask if employee would like to immunize
            Console.WriteLine("Enter animal ID to confirm they're immunized.");
            userInput = Console.ReadLine();
            bool willImmunize = false;
            database.ChangeBoolStatus("Animals", "HasShots", (willImmunize ? 1 : 0), Convert.ToInt32(userInput));

            animals = database.SearchAnimals();
            immunizedAnimals = animals.Where(m => m.IsImmunized == false).OrderBy(m => m.AnimalID);
            foreach (Animal animal in immunizedAnimals)
            {
                Console.WriteLine("(Animal ID: " + animal.AnimalID + ") " + animal.Name + (animal.IsImmunized ? " is immunized" : " is not immunized"));
            }
            //test to see if animals are returned
        }
    }
}
