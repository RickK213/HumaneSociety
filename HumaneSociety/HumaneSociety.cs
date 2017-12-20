using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public class HumaneSociety
    {
        string userInput;
        public HumaneSociety()
        {

        }
        public void Start()
        {
            UI.DisplayIntroScreen();
            UI.DisplayMenuHeader();
            userInput = UI.GetValidUserOption("1: Continue as an Adopter\n2: Continue as an Employee", new List<string>() { "1", "2" });
            if (userInput == "1")
            {
                //create a new adopter
            }
            else
            {
                //create a new employee
            }
        }
    }
}
