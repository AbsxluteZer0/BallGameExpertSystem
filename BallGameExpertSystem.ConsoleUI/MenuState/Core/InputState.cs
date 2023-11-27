using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallGameExpertSystem.ConsoleUI.MenuState.Core
{
    abstract class InputState : IState
    {
        protected virtual void ShowMenu()
        {
            Console.WriteLine("Очікування введення:");
        }

        protected virtual string Read()
        {
            ShowMenu();
            return Console.ReadLine();
        }

        protected abstract IState NextState(string str);

        protected virtual void Continue()
        {
            Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
            Console.ReadKey();
        }

        public virtual IState RunState()
        {
            var input = Read();
            return NextState(input);
        }
    }
}
