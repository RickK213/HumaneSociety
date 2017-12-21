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
        //As a developer, I want to use LINQ to import a CSV file that already contains data about several animals
        //that are being transferred from a different humane society.

        //direct path to file:
        //C:\Users\Rick Kippert\Dropbox\_devCodeCamp\Assignments\week9-2-humane_society\cSharp-HumaneSociety

        //Current path:
        //C:\Users\Rick Kippert\Dropbox\_devCodeCamp\Assignments\week9-2-humane_society\cSharp-HumaneSociety\HumaneSociety\bin\Debug

        //Relative path:
        //../../../animals.csv


        //member variables
        string filePath = @"../../../animals.csv";
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
            bool fileExists = File.Exists(filePath);
            if (fileExists)
            {
                Console.WriteLine("Good News! The file at the relative path: " + filePath + " exists!\nPress any key to import the CSV into your database!");
                Console.ReadKey();
                ImportCSV();
            }
            else
            {
                Console.WriteLine("Sorry. I could not find the file {0}.\nPress Any Key to quit the application, add the 'animals.csv' file and try again!", filePath);
                Console.ReadKey();
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

        void ImportCSV()
        {
            Console.Clear();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Each row:
                    //name	species	roomNumber	hasShots	price	foodPerWeek
                    string[] fields = parser.ReadFields();
                    animal = animalFactory.CreateAnimal(fields[1]);
                    animal.Name = fields[0];
                    animal.RoomNumber = Convert.ToInt32(fields[2]);
                    animal.IsImmunized = GetBoolFromYesorNo(fields[3]);
                    animal.Price = Convert.ToDouble(fields[4]);
                    animal.Price = Convert.ToInt32(fields[5]);
                    database.AddAnimal(animal);

                }
            }
            Console.WriteLine("File imported!");
        }

    }
}
