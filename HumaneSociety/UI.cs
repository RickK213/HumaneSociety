﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
            string menuOptions = GetAddAnimalMenu(allowAll);
            DisplayMenu(menuOptions);
            List<string> menuNumbers = GetMenuNumbers(menuOptions);
            DisplayMenuHeader();
            return GetValidUserOption("", menuNumbers);
        }

        public static void GetAnyKeyToContinue(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey();
        }

        static bool isStateAbbreviation(String state)
        {
            string states = "|AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY|";
            return state.Length == 2 && states.IndexOf(state) > 0;
        }

        public static string GetState()
        {
            Console.WriteLine("Enter your State (Abbreviated - i.e. 'WI'):");
            string state = Console.ReadLine();
            if ( !isStateAbbreviation(state) )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You must enter a valid State abbreviation. Please read the instructions.");
                Console.ResetColor();
                return GetState();
            }
            return state;
        }

        static bool IsZipCode(string zipCode)
        {
            string pattern = @"^\d{5}(?:[-\s]\d{4})?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(zipCode);
        }

        public static string GetZipCode()
        {
            Console.WriteLine("Enter your Zip Code:");
            string zipCode = Console.ReadLine();
            if ( !IsZipCode(zipCode) )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You must enter a valid U.S. Zip Code.");
                Console.ResetColor();
                return GetZipCode();
            }
            return zipCode;
        }

        public static string GetCity()
        {
            Console.WriteLine("Enter your City:");
            return Console.ReadLine();
        }

        public static string GetStreetOne()
        {
            Console.WriteLine("Enter your Street Address:");
            return Console.ReadLine();
        }


        static bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static string GetAdopterEmail()
        {
            Console.WriteLine("Enter your Email Address:");
            string emailAddress = Console.ReadLine();
            if (!IsValidEmailAddress(emailAddress))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You must enter a valid email address.");
                Console.ResetColor();
                return GetAdopterEmail();
            }
            return emailAddress;
        }

        public static string GetAdopterName()
        {
            Console.WriteLine("Enter your Name:");
            return Console.ReadLine();
        }

        public static string GetAnimalName(bool allowNone)
        {
            Console.Write("\nEnter an animal Name");
            if (allowNone)
            {
                Console.WriteLine(" (or leave blank)");
            }
            Console.WriteLine(":");
            string name = Console.ReadLine();
            if (name.Length==0 && !allowNone)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You must enter an animal name.");
                Console.ResetColor();
                return GetAnimalName(allowNone);
            }
            return name;
        }

        public static string GetFoodPerWeek(bool allowNone)
        {
            Console.Write("\nEnter the amount of food consumer per week in ounces. Must be an integer");
            if (allowNone)
            {
                Console.WriteLine(" (or leave blank)");
            }
            Console.WriteLine(":");
            string userInput = Console.ReadLine();
            int foodPerWeek;
            if ( (!int.TryParse(userInput, out foodPerWeek)) && !allowNone)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter an integer!");
                Console.ResetColor();
                return GetFoodPerWeek(allowNone);
            }
            else
            {
                return foodPerWeek.ToString();
            }
        }

        public static string GetAnimalPrice(bool isSearchingAnimal)
        {
            if ( !isSearchingAnimal)
            {
                Console.WriteLine("Enter the price of the animal in dollars (i.e. 45.50)");
            }
            else
            {
                Console.WriteLine("Enter the maximum price for your search (i.e. 45.50)");
            }
            string userInput = Console.ReadLine();
            if ( isSearchingAnimal && userInput.Length==0 )
            {
                return userInput;
            }
            double animalPrice;
            if (!double.TryParse(userInput, out animalPrice))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a dollar amount. Be sure leave off '$' and include the decimal point");
                Console.ResetColor();
                return GetAnimalPrice(isSearchingAnimal);
            }
            else
            {
                return animalPrice.ToString();
            }

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
                Console.WriteLine("Please enter a number!");
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

        static string GetMainMenu(string role)
        {
            StringBuilder menuOptions = new StringBuilder();
            if (role == "employee")
            {
                menuOptions.Append("1: Add Animal to Database\n");
                menuOptions.Append("2: Search Animals\n");
                menuOptions.Append("3: List All Animals\n");
                menuOptions.Append("4: Search Adopters\n");
                menuOptions.Append("5: List All Adopters\n");
                menuOptions.Append("6: Import Animals from CSV\n");
            }
            else
            {
                menuOptions.Append("1: Create Profile\n");
                menuOptions.Append("2: Search Animals\n");
                menuOptions.Append("3: List All Animals\n");
            }
            menuOptions.Append("q: Quit Application");
            return menuOptions.ToString();
        }

        static string GetAddAnimalMenu(bool allowAll)
        {
            StringBuilder menuOptions = new StringBuilder();
            menuOptions.Append("1: Dog\n");
            menuOptions.Append("2: Cat\n");
            menuOptions.Append("3: Bird\n");
            menuOptions.Append("4: Rabbit\n");
            menuOptions.Append("5: Ferret\n");
            if (allowAll)
            {
                menuOptions.Append("6: All\n");
            }
            menuOptions.Append("m: Return to Main Menu\n");
            menuOptions.Append("q: Quit Application");
            return menuOptions.ToString();
        }

        public static void DisplayPageHeader(string header)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(header.ToUpper());
            Console.ResetColor();
        }

        static List<string> GetMenuNumbers(string menuOptions)
        {
            List<string> menuNumbers = new List<string>();
            string[] menuLines = menuOptions.Split(
                new[] { "\n" },
                StringSplitOptions.None
            );
            for (int i=0; i<menuLines.Length; i++ )
            {
                menuNumbers.Add(menuLines[i][0].ToString());
            }
            return menuNumbers;
        }

        static void DisplayMenu(string menuOptions)
        {
            Console.WriteLine(menuOptions);
        }

        public static string GetMainMenuOptions(string role)
        {
            DisplayPageHeader(role + " MAIN MENU");
            string menuOptions = GetMainMenu(role);
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
