using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;
using System.Collections.Generic;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class B5State : MenuState
    {
        private BallGameExpertSystem expertSystem;

        public B5State(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        private Dictionary<int, MenuItem> menus = new Dictionary<int, MenuItem>() {
        {1, new MenuItem(){Text = "Білий"}},
        {2, new MenuItem(){Text = "Оранжевий"}},
        {3, new MenuItem(){Text = "Жовтий"}}
        };

        protected override Dictionary<int, MenuItem> Menus => menus;

        protected override IState NextState(KeyValuePair<int, MenuItem> selectedMenu)
        {
            expertSystem.SetBallColor(selectedMenu.Key);
            return new P1State(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Колір м'яча:");
            base.ShowMenu();
        }
    }
}