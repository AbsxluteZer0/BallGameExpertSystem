using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class FinalState : InteractionState
    {
        private BallGameExpertSystem expertSystem;
        public FinalState(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        public override IState RunState()
        {
            string conclusion = expertSystem.Conclude();
            Console.WriteLine("Висновок: " + conclusion);
            Continue();
            return null;
        }
    }
}
