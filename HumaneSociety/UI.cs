using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class UI
    {
        public static void DisplayIntroScreen()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("WELCOME TO A.R. HUMANE SOCIETY");
            Console.ResetColor();
            Console.WriteLine("Instructions to come...");

        }

        public static string GetAddAnimalOption(bool allowAll)
        {
            List<string> menuOptions = GetAddAnimalMenu(allowAll);
            DisplayMenu(menuOptions);
            List<string> menuNumbers = GetMenuNumbers(menuOptions);
            DisplayMenuHeader();
            return GetValidUserOption("", menuNumbers);
        }

        public static string GetAnimalName(bool allowNone)
        {
            Console.Write("\nEnter an animal Name");
            if (allowNone)
            {
                Console.WriteLine(" (or leave blank)");
            }
            Console.WriteLine(":");
            return Console.ReadLine();
        }

        public static string GetAnimalRoomNumber(bool allowNone)
        {
            Console.Write("\nEnter a room number for the animal");
            if (allowNone)
            {
                Console.WriteLine(" (or leave blank)");
            }
            Console.WriteLine(":");
            string userInput = Console.ReadLine();
            int roomNumber;
            if (!int.TryParse(userInput, out roomNumber))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter number!");
                Console.ResetColor();
                return GetAnimalRoomNumber(allowNone);
            }
            else
            {
                return roomNumber.ToString();
            }
        }

        public static string GetImmunizationStatus(bool isSearchingAnimal)
        {
            if (!isSearchingAnimal)
            {
                Console.WriteLine("\nHas the animal been immunized?");
            }
            else
            {
                Console.WriteLine("\nLimit search to immunized animals?");
            }
            return GetValidUserOption("", new List<string> {"y", "n"});
        }

        public static void DisplayMenuHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nMenu Options==================================");
            Console.ResetColor();
        }

        public static void DisplayValidOptions(List<string> validOptions)
        {
            Console.Write("Enter ");
            for (int i = 0; i < validOptions.Count; i++)
            {
                if (i == validOptions.Count - 1)
                {
                    Console.Write(" or ");
                }
                Console.Write("'");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(validOptions[i]);
                Console.ResetColor();
                Console.Write("'");
                if (i < validOptions.Count - 2)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }


        static List<string> GetMainMenu(string role)
        {
            List<string> menuOptions = new List<string>();
            if (role == "employee")
            {
                menuOptions.Add("Add Animal to Database");
                menuOptions.Add("Search Animals");
                menuOptions.Add("List All Animals");
                menuOptions.Add("Search Adopters");
                menuOptions.Add("List All Adopters");
            }
            else
            {
                menuOptions.Add("Create/Edit Profile");
                menuOptions.Add("Search Animals");
                menuOptions.Add("List All Animals");
            }
            return menuOptions;
        }

        static List<string> GetAddAnimalMenu(bool allowAll)
        {
            List<string> menuOptions = new List<string>();
            menuOptions.Add("Dog");
            menuOptions.Add("Cat");
            menuOptions.Add("Bird");
            menuOptions.Add("Rabbit");
            menuOptions.Add("Ferret");
            if (allowAll)
            {
                menuOptions.Add("All");
            }
            return menuOptions;
        }

        public static void DisplayPageHeader(string header)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(header.ToUpper());
            Console.ResetColor();
        }

        static List<string> GetMenuNumbers(List<string> menuOptions)
        {
            List<string> menuNumbers = new List<string>();
            for (int i = 0; i < menuOptions.Count; i++)
            {
                menuNumbers.Add(i.ToString());
            }
            return menuNumbers;
        }

        static void DisplayMenu(List<string> menuOptions)
        {
            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.WriteLine("{0}: {1}", i.ToString(), menuOptions[i]);
            }
        }

        public static string GetMainMenuOptions(string role)
        {
            DisplayPageHeader(role + " MAIN MENU");
            List<string> menuOptions = GetMainMenu(role);
            DisplayMenu(menuOptions);
            List<string> menuNumbers = GetMenuNumbers(menuOptions);
            DisplayMenuHeader();
            return GetValidUserOption("", menuNumbers);
        }

        public static string GetValidUserOption(string instruction, List<string> validOptions)
        {
            Console.WriteLine(instruction);
            DisplayValidOptions(validOptions);
            string userInput = Console.ReadLine();
            if (!validOptions.Contains(userInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("'{0}' is an invalid option. Please read the instructions.", userInput);
                Console.ResetColor();
                return GetValidUserOption(instruction, validOptions);
            }
            Console.WriteLine();
            return userInput;
        }


    }
}
