using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace HumaneSociety
{
    public class CSVReader
    {
        //direct path to file:
        //C:\Users\Rick Kippert\Dropbox\_devCodeCamp\Assignments\week9-2-humane_society\cSharp-HumaneSociety

        //Current path:
        //C:\Users\Rick Kippert\Dropbox\_devCodeCamp\Assignments\week9-2-humane_society\cSharp-HumaneSociety\HumaneSociety\bin\Debug

        //Relative path:
        //../../../animals.csv

        //member variables
        string filePath = @"../../../animals.csv";

        //Test CSVs:
        //string filePath = @"../../../animals-one_empty_field.csv";
        //string filePath = @"../../../animals-invalid_species.csv";
        //string filePath = @"../../../animals-invalid_and_empty_fields.csv";

        public DatabaseControl database = new DatabaseControl();
        public AnimalFactory animalFactory = new ConcreteAnimalFactory();
        public Animal animal = null;

        //constructor
        public CSVReader()
        {

        }

        //member methods
        public void Start()
        {
            UI.DisplayPageHeader("Import animals from CSV");
            bool fileExists = File.Exists(filePath);
            if (fileExists)
            {
                Console.WriteLine("\nGood News! The file at the relative path: " + filePath + " exists!\nPress any key to import the CSV into your database!");
                Console.ReadKey(true);
                ImportCSV();
            }
            else
            {
                Console.WriteLine("\nSorry. I could not find the file {0}.\nPress Any Key to quit the application, add the 'animals.csv' file and try again!", filePath);
                Console.ReadKey(true);
                Environment.Exit(-1);
            }
        }

        bool GetBoolFromYesorNo(string yesNo)
        {
            if (yesNo.ToLower() == "yes")
            {
                return true;
            }
            return false;
        }

        public bool ImportCSV()
        {
            //TO DO: ADD try, catch, finally BELOW
            List<Animal> rawAnimals = new List<Animal>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Each row:
                    //name	species	roomNumber	hasShots	price	foodPerWeek
                    string[] fields = parser.ReadFields();
                    if ( fields.Any(x => x.Length==0) )
                    {
                        Console.WriteLine("We found an empty value in your CSV. Please check your file and try again.\nPress any key to return to main menu.");
                        Console.ReadKey(true);
                        return false;
                    }
                    animal = animalFactory.CreateAnimal(fields[1]);
                    animal.Name = fields[0];
                    animal.RoomNumber = Convert.ToInt32(fields[2]);
                    animal.IsImmunized = GetBoolFromYesorNo(fields[3]);
                    animal.Price = Convert.ToDouble(fields[4]);
                    animal.OunceFoodPerWeek = Convert.ToInt32(fields[5]);
                    rawAnimals.Add(animal);
                }
            }
            List<Animal> validAnimals;
            validAnimals = rawAnimals.Where(
                x =>
                (x.Name.Length > 0) &&
                (x.Species.Length > 0) &&
                (x.RoomNumber > 0) &&
                (x.Price > 0) &&
                (x.OunceFoodPerWeek > 0)
                ).ToList();

            int numberOfInvalidRows = rawAnimals.Count - validAnimals.Count;
            bool animalsAddedSuccessfully = true;
            if ( numberOfInvalidRows > 0 )
            {
                Console.WriteLine("\n{0} rows in your CSV contained errors. Please check your file and try again.\nPress any key to return to main menu", numberOfInvalidRows);
                Console.ReadKey(true);
                return false;
            }
            else
            {
                foreach (Animal animal in validAnimals)
                {
                    if (animalsAddedSuccessfully)
                    {
                        animalsAddedSuccessfully = database.AddAnimal(animal);
                    }
                }
            }
            if (animalsAddedSuccessfully)
            {
                Console.WriteLine("\nSuccess! {0} animals have been imported to your database!\nPress any key to return to main menu.", validAnimals.Count);
            }
            else
            {
                Console.WriteLine("\nDuplicate room numbers were found, not all animals were added or this CSV was already imported.\nPress any key to return to main menu.");
            }
                Console.ReadKey(true);
            return true;
        }

    }
}
