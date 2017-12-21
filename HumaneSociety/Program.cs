using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            HumaneSociety humaneSociety = new HumaneSociety();
            humaneSociety.Start();

            ////test animal, remove later
            //test animal, remove later
            //Animal animal = new Dog();
            //animal.Name = "Fido";
            //animal.IsAdopted = false;
            //animal.IsImmunized = true;
            //animal.OunceFoodPerWeek = 27;
            //animal.Price = 25.06;
            //animal.RoomNumber = 3;
            ////-/remove
            //User adopter = new Adopter();
            //adopter.ListAnimals();
            //-/remove
            //DatabaseControl database = new DatabaseControl();
            //database.SearchAnimals(animal);

            //CSV stuff:
            //CSVReader csvReader = new CSVReader();
            //csvReader.Start();

            Console.ReadKey();
        }
    }
}
