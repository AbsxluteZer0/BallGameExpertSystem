using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;
using System.Collections.Generic;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class P3State : MenuState
    {
        private BallGameExpertSystem expertSystem;

        public P3State(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        private Dictionary<int, MenuItem> menus = new Dictionary<int, MenuItem>() {
        {1, new MenuItem(){Text = "Газон"}},
        {2, new MenuItem(){Text = "Ґрунт"}},
        {3, new MenuItem(){Text = "Гума"}},
        {4, new MenuItem(){Text = "Ламінат"}}
        };

        protected override Dictionary<int, MenuItem> Menus => menus;

        protected override IState NextState(KeyValuePair<int, MenuItem> selectedMenu)
        {
            expertSystem.SetCovering(selectedMenu.Key);
            return new P4State(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Покриття:");
            base.ShowMenu();
        }
    }
}