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
            //HumaneSociety humaneSociety = new HumaneSociety();
            //humaneSociety.Start();
            //test animal, remove later
            Animal animal = new Dog();
            animal.Name = "Fido";
            animal.IsAdopted = false;
            animal.IsImmunized = true;
            animal.OunceFoodPerWeek = 27;
            animal.Price = 25;
            animal.RoomNumber = 3;
            //-/remove
            DatabaseControl database = new DatabaseControl();
            database.AddAnimal(animal);
            Console.ReadKey();
        }
    }
}
