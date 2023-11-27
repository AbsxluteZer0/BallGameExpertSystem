using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class T2State : InputState
    {
        private BallGameExpertSystem expertSystem;

        public T2State(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        protected override string Read()
        {
            ShowMenu();

            var str = Console.ReadLine();
            int answer = 0;

            if (int.TryParse(str, out answer))
            {
                return str;
            }
            else
            {
                Console.WriteLine("Ви ввели не число.");
                return Read();
            }
        }

        protected override IState NextState(string str)
        {
            expertSystem.PlayersInTeam = int.Parse(str);
            return new FinalState(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Кількість гравців у команді:");
        }
    }
}