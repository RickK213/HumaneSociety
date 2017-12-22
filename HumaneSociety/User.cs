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
        int adopterID;
        string name;
        string email;
        string streetAddress;
        string city;
        string state;
        string zipCode;
        int adoptedAnimalID;
        bool hasPaid = false;

        //properties
        public int AdopterID
        {
            get
            {
                return adopterID;
            }
            set
            {
                adopterID = value;
            }
        }
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
        public int AdoptedAnimalID
        {
            get
            {
                return adoptedAnimalID;
            }
            set
            {
                adoptedAnimalID = value;
            }
        }

        public bool HasPaid {
            get
            {
                return hasPaid;
            }
            set
            {
                hasPaid = value;
            }
        }

        //constructor
        public User()
        {

        }

        void CreateProfile()
        {
            UI.DisplayPageHeader("Create Profile");
            Name = UI.GetAdopterName();
            Email = UI.GetAdopterEmail();
            StreetAddress = UI.GetStreetOne();
            City = UI.GetCity();
            State = UI.GetState();
            ZipCode = UI.GetZipCode();
            database.SaveAdopter(this);
            UI.GetAnyKeyToContinue("Profile Saved. Press any key to return to main menu.");
        }

        public virtual void StartFlow()
        {
            //Main menu
            string userInput = UI.GetMainMenuOptions(role);

            switch (userInput)
            {
                case ("1"):
                    CreateProfile();
                    StartFlow();
                    break;
                case ("2"):
                    SearchAnimals();
                    StartFlow();
                    break;
                case ("3"):
                    ListAnimals();
                    StartFlow();
                    break;
                case ("q"):
                    Environment.Exit(-1);
                    break;
            }
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

        public virtual void SearchAnimals()
        {
            userInput = UI.GetSearchOption(role);

            switch (userInput)
            {
                case ("1"):
                    string nameToSearch = UI.GetAnimalName(false);
                    SearchByName(nameToSearch);
                    StartFlow();
                    break;
                case ("2"):
                    string speciesToSearch = UI.GetSpeciesName();
                    SearchBySpecies(speciesToSearch);
                    StartFlow();
                    break;
                case ("3"):
                    string immunizationToSearch = UI.GetImmunizationStatus(true);
                    SearchByImmunization(immunizationToSearch);
                    StartFlow();
                    break;
                case ("4"):
                    string priceMax = UI.GetAnimalPrice(true);
                    SearchByMaxPrice(priceMax);
                    StartFlow();
                    break;
                case ("5"):
                    SearchByMultipleCriteria();
                    StartFlow();
                    break;
                case ("m"):
                    StartFlow();
                    break;
                case ("q"):
                    Environment.Exit(-1);
                    break;
            }

        }

        public void ListAnimals()
        {
            UI.DisplayPageHeader("All Animals");
            List<Animal> animals = database.GetAllAnimals();
            if ( animals.Count > 0)
            {
                UI.DisplayList(animals);
                EditAnimalFromList(animals);
            }
            else
            {
                UI.GetAnyKeyToContinue("No animals in database. Press any key to return to main menu.");
                return;
            }
        }

        public virtual List<Animal> SearchByMultipleCriteria()
        {
            bool isSearchingByName = false;
            bool isSearchingBySpecies = false;
            bool isSearchingByImmunization = false;
            bool isSearchingByPrice = false;
            bool isSearchingByAdoptionStatus = false;

            string nameToSearch = null;
            string speciesToSearch = null;
            bool statusOfImmunization = false;
            double priceAmountToSearch = 0;
            bool animalIsAdopted = false;

            //PUT THE NEXT 5 OR SO LINES IN UI?
            UI.DisplayPageHeader("Search by Multiple Criteria");
            Console.WriteLine("Enter the criteria you would like to search for seperated by a comma.");
            Console.WriteLine("Options include 'name', 'species', 'immunization', 'price'");
            Console.WriteLine("Example: price,name,species");

            //add verification for termsToSearch
            int numberOfSearchOptionsAccepted = 0;
            string termsToSearch = Console.ReadLine();
            List<string> userSearchOptions = termsToSearch.Split(',').ToList();
            if(userSearchOptions.Remove("name"))
            {
                numberOfSearchOptionsAccepted += 1;
                isSearchingByName = true;
                Console.WriteLine("Search by name:");
                nameToSearch = UI.GetAnimalName(false);// Console.ReadLine();
            }
            if (userSearchOptions.Remove("species"))
            {
                numberOfSearchOptionsAccepted += 1;
                isSearchingBySpecies = true;
                Console.WriteLine("Search by species:");
                speciesToSearch = UI.GetValidUserOption("What is the species of animal you are looking for? (cat, dog, bird, rabbit or ferret)", new List<string> { "cat", "dog", "ferret", "rabbit", "bird" }); ;
            }
            if (userSearchOptions.Remove("immunization"))
            {
                numberOfSearchOptionsAccepted += 1;
                isSearchingByImmunization = true;
                userInput = UI.GetImmunizationStatus(true);
                if (userInput == "y")
                {
                    statusOfImmunization = true;
                }
                else
                {
                    statusOfImmunization = false;
                }
            }
            if(userSearchOptions.Remove("price"))
            {
                numberOfSearchOptionsAccepted += 1;
                isSearchingByPrice = true;
                userInput = UI.GetAnimalPrice(true);
                priceAmountToSearch = Convert.ToDouble(userInput);
            }

            if (numberOfSearchOptionsAccepted == 0)
            {
                Console.WriteLine("None of your inputs were recognized.  Please try again.");
                Console.ReadKey();
                return SearchByMultipleCriteria();                
            }
            else
            {
                List<Animal> animals = database.GetAllAnimals();
                List<Animal> foundAnimals;
                foundAnimals = animals.Where(
                    m =>
                    (isSearchingByName ? m.Name.ToLower() == nameToSearch.ToLower() : m.Name != null) &&
                    (isSearchingBySpecies ? m.Species == speciesToSearch : m.Species != null) &&
                    (isSearchingByImmunization ? m.IsImmunized == statusOfImmunization : m.IsImmunized != null) &&
                    (isSearchingByPrice ? m.Price < priceAmountToSearch : m.Price > 0) &&
                    (isSearchingByAdoptionStatus ? m.IsAdopted == animalIsAdopted : m.IsAdopted != null)
                    ).OrderBy(m => m.AnimalID).ToList();

                UI.DisplayList(foundAnimals);
                return foundAnimals;
            }
        }

        public void EditAnimalFromList(List<Animal> animals)
        {
            if(this.role == "adopter")
            {
                UI.GetAnyKeyToContinue("Press any key to return to the main menu.");
                return;
            }
            userInput = UI.GetAnimalToEdit(animals);
            if (userInput == "")
            {
                return;
            }
            Animal animalToEdit;
            animalToEdit = animals.Where(x => x.AnimalID.ToString() == userInput).ToList().First();
            EditAnimal(animalToEdit);
        }

        public void SearchByName(string nameToSearch)
        {
            List<Animal> animals = database.GetAllAnimals();
            List<Animal> foundAnimals;
            foundAnimals = animals.Where(m => (m.Name.ToLower() == nameToSearch.ToLower())).ToList();
            UI.DisplayList(foundAnimals);
            EditAnimalFromList(foundAnimals);
        }

        public void SearchBySpecies(string speciesToSearch)
        {
            List<Animal> animals = database.GetAllAnimals();
            List<Animal> foundAnimals;
            foundAnimals = animals.Where(m => (m.Species.ToLower() == speciesToSearch.ToLower())).ToList();
            UI.DisplayList(foundAnimals);
            EditAnimalFromList(foundAnimals);
        }

        public void SearchByImmunization(string immunizationToSearch)
        {
            List<Animal> animals = database.GetAllAnimals();
            List<Animal> foundAnimals;
            bool resultStatus = false;
            if (immunizationToSearch == "y")
            {
                resultStatus = true;
            }
            foundAnimals = animals.Where(m => (m.IsImmunized == resultStatus)).ToList();
            UI.DisplayList(foundAnimals);
            EditAnimalFromList(foundAnimals);
        }

        public void SearchByAdoption(string adoptionToSearch)
        {
            List<Animal> animals = database.GetAllAnimals();
            List<Animal> foundAnimals;
            bool resultStatus = false;
            if (adoptionToSearch == "y")
            {
                resultStatus = true;
            }
            foundAnimals = animals.Where(m => (m.IsAdopted == resultStatus)).ToList();
            UI.DisplayList(foundAnimals);
            EditAnimalFromList(foundAnimals);
        }

        public void SearchByMaxPrice(string maxPrice)
        {
            List<Animal> animals = database.GetAllAnimals();
            List<Animal> foundAnimals;
            foundAnimals = animals.Where(m => (m.Price < Convert.ToDouble(maxPrice) )).ToList();
            UI.DisplayList(foundAnimals);
            EditAnimalFromList(foundAnimals);
        }

        public void EditAnimal(Animal animal)
        {
            UI.DisplayPageHeader(String.Format("Edit Animal"));
            UI.DisplaySingleObject(animal);
            Console.WriteLine("Enter 'i' to switch immunization status to {0}", !animal.IsImmunized);
            Console.WriteLine("Enter 'a' to switch addoption status to {0}", !animal.IsAdopted);
            Console.WriteLine("Or press enter to return to main menu");
            userInput = UI.GetValidUserOption("", new List<string>() {"i", "a", "" });
            bool changeTo;
            switch (userInput)
            {
                case ("i"):
                    changeTo = !animal.IsImmunized;
                    database.ChangeSingleValue("Animals", "HasShots", (changeTo ? 1 : 0), "AnimalID", animal.AnimalID);
                    UI.GetAnyKeyToContinue("Immunization status changed. Press any key to return to main menu.");
                    return;
                case ("a"):
                    changeTo = !animal.IsAdopted;
                    database.ChangeSingleValue("Animals", "IsAdopted", (changeTo ? 1 : 0), "AnimalID", animal.AnimalID);
                    UI.GetAnyKeyToContinue("Adoption status changed. Press any key to return to main menu.");
                    return;
                case (""):
                    return;
                default:
                    return;
            }
        }

    }
}
