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


        public static string GetMainMenuOptions(string role)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} MAIN MENU\n", role.ToUpper());
            Console.ResetColor();
            List<string> menuOptions = new List<string>();
            if ( role == "employee" )
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
            List<string> menuNumbers = new List<string>();
            for ( int i=0; i<menuOptions.Count; i++ )
            {
                Console.WriteLine("{0}: {1}", i, menuOptions[i]);
                menuNumbers.Add(i.ToString());
            }
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
