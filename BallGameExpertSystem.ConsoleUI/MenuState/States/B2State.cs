using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;
using System.Collections.Generic;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class B2State : MenuState
    {
        private BallGameExpertSystem expertSystem;

        public B2State(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        private Dictionary<int, MenuItem> menus = new Dictionary<int, MenuItem>() {
        {1, new MenuItem(){Text = "Шкіра"}},
        {2, new MenuItem(){Text = "Гума"}}
        };

        protected override Dictionary<int, MenuItem> Menus => menus;

        protected override IState NextState(KeyValuePair<int, MenuItem> selectedMenu)
        {
            expertSystem.SetBallMaterial(selectedMenu.Key);
            return new B3State(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Матеріал м'яча:");
            base.ShowMenu();
        }
    }
}
