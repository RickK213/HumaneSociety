using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public class HumaneSociety
    {
        User user;
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
                user = new Adopter();
            }
            else
            {
                user = new Employee();
            }
            user.StartFlow();
        }

        public void Start_Rick()
        {
            UI.DisplayIntroScreen();
            UI.DisplayMenuHeader();
            userInput = UI.GetValidUserOption("1: Continue as an Adopter\n2: Continue as an Employee", new List<string>() { "1", "2" });
            if (userInput == "1")
            {
                user = new Adopter();
            }
            else
            {
                user = new Employee();
            }
            userInput = UI.GetMainMenuOptions(user.role);
        }
    }
}
