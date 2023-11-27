using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;
using System.Collections.Generic;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class P2State : MenuState
    {
        private BallGameExpertSystem expertSystem;

        public P2State(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        private Dictionary<int, MenuItem> menus = new Dictionary<int, MenuItem>() {
        {1, new MenuItem(){Text = "Під відкритим небом"}},
        {2, new MenuItem(){Text = "У приміщенні"}}
        };

        protected override Dictionary<int, MenuItem> Menus => menus;

        protected override IState NextState(KeyValuePair<int, MenuItem> selectedMenu)
        {
            expertSystem.SetWhereIs(selectedMenu.Key);
            return new P3State(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Де знаходиться:");
            base.ShowMenu();
        }
    }
}