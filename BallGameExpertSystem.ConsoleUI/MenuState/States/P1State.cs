using BallGameExpertSystem.ConsoleUI.MenuState.Core;
using System;
using System.Collections.Generic;

namespace BallGameExpertSystem.ConsoleUI.MenuState.States
{
    class P1State : MenuState
    {
        private BallGameExpertSystem expertSystem;

        public P1State(BallGameExpertSystem expertSystem)
        {
            this.expertSystem = expertSystem;
        }

        private Dictionary<int, MenuItem> menus = new Dictionary<int, MenuItem>() {
        {1, new MenuItem(){Text = "Стадіон"}},
        {2, new MenuItem(){Text = "Майданчик"}},
        {3, new MenuItem(){Text = "Корт"}}
        };

        protected override Dictionary<int, MenuItem> Menus => menus;

        protected override IState NextState(KeyValuePair<int, MenuItem> selectedMenu)
        {
            expertSystem.SetGroundType(selectedMenu.Key);
            return new P2State(expertSystem);
        }

        protected override void ShowMenu()
        {
            Console.WriteLine("Оберіть параметри місця проведення гри");
            Console.WriteLine("Тип майданчика:");
            base.ShowMenu();
        }
    }
}