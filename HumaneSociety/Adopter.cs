using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public class Adopter : User
    {
        public Adopter()
        {
            role = "adopter";
        }

        public override void StartFlow()
        {
            //Main menu
            string userInput = UI.GetMainMenuOptions(role);

        }

    }
}
