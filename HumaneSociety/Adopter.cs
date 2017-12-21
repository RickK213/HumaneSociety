using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public class Adopter : User
    {
        //member variables

        //constructor
        public Adopter()
        {
            role = "adopter";
        }

        //member methods
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
        }

        public override void StartFlow()
        {
            //Main menu
            string userInput = UI.GetMainMenuOptions(role);

            switch (userInput)
            {
                case ("1"):
                    CreateProfile();
                    break;
                case ("2"):
                    SearchAnimals();
                    break;
                case ("3"):
                    ListAnimals();
                    break;
                case ("q"):
                    Environment.Exit(-1);
                    break;
            }
        }

    }
}
