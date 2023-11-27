using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;
using System.Collections.Generic;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class P4State : MenuState
    {
        private BallGameExpertSystem expertSystem;

        public P4State(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        private Dictionary<int, MenuItem> menus = new Dictionary<int, MenuItem>() {
        {1, new MenuItem(){Text = "Мала"}},
        {2, new MenuItem(){Text = "Середня"}},
        {3, new MenuItem(){Text = "Велика"}}
        };

        protected override Dictionary<int, MenuItem> Menus => menus;

        protected override IState NextState(KeyValuePair<int, MenuItem> selectedMenu)
        {
            expertSystem.SetArea(selectedMenu.Key);
            return new T1State(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Площа:");
            base.ShowMenu();
        }
    }
}