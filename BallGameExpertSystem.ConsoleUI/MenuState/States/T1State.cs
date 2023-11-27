using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class T1State : InputState
    {
        private BallGameExpertSystem expertSystem;

        public T1State(BallGameExpertSystem expertSystem)
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
            expertSystem.NumberOfTeams = int.Parse(str);
            return new T2State(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Введіть параметри гравців");
            Console.WriteLine("Кількість команд:");
        }
    }
}