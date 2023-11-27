using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallGameExpertSystem.ConsoleUI.MenuState.Core
{
    abstract class InteractionState : IState
    {
        protected virtual void Continue()
        {
            Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
            Console.ReadKey();
        }
        public abstract IState RunState();
    }
}
