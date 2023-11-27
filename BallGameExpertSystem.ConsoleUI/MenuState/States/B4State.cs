using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;
using System.Collections.Generic;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class B4State : MenuState
    {
        private BallGameExpertSystem expertSystem;

        public B4State(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        private Dictionary<int, MenuItem> menus = new Dictionary<int, MenuItem>() {
        {1, new MenuItem(){Text = "Низька"}},
        {2, new MenuItem(){Text = "Середня"}},
        {3, new MenuItem(){Text = "Висока"}}
        };

        protected override Dictionary<int, MenuItem> Menus => menus;

        protected override IState NextState(KeyValuePair<int, MenuItem> selectedMenu)
        {
            expertSystem.SetBallElasticity(selectedMenu.Key);
            return new B5State(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Пружність м'яча:");
            base.ShowMenu();
        }
    }
}
