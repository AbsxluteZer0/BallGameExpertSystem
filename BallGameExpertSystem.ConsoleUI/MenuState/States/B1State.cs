using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;
using System.Collections.Generic;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    public class B1State : MenuState
    {
        private BallGameExpertSystem expertSystem;

        public B1State(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        private Dictionary<int, MenuItem> menus = new Dictionary<int, MenuItem>() {
        {1, new MenuItem(){Text = "Кругла"}},
        {2, new MenuItem(){Text = "Овальна"}}
        };

        protected override Dictionary<int, MenuItem> Menus => menus;

        protected override IState NextState(KeyValuePair<int, MenuItem> selectedMenu)
        {
            expertSystem.SetBallShape(selectedMenu.Key);
            return new B2State(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Вас вітає експертна система в галузі спортивних ігор з м'ячем!");
            Console.WriteLine("Будь ласка, оберіть параметри м'яча.");
            Console.WriteLine("Форма м'яча:");
            base.ShowMenu();
        }
    }
}
