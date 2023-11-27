using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;
using System.Collections.Generic;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class B3State : MenuState
    {
        private BallGameExpertSystem expertSystem;

        public B3State(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        private Dictionary<int, MenuItem> menus = new Dictionary<int, MenuItem>() {
        {1, new MenuItem(){Text = "Малий"}},
        {2, new MenuItem(){Text = "Середній"}},
        {3, new MenuItem(){Text = "Великий"}}
        };

        protected override Dictionary<int, MenuItem> Menus => menus;

        protected override IState NextState(KeyValuePair<int, MenuItem> selectedMenu)
        {
            expertSystem.SetBallSize(selectedMenu.Key);
            return new B4State(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Розмір м'яча:");
            base.ShowMenu();
        }
    }
}
