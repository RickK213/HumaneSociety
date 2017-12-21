using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //constructor
        public CSVReader()
        {

        }

        //member methods
        public void Start()
        {
            string filePath = @"../../../animals.csv";
            bool fileExists = File.Exists(filePath);
            Console.WriteLine(fileExists ? "Good News! The file at the relative path: " + filePath +  " exists!" : "File does not exist.");
        }

    }
}
