using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallGameExpertSystem.ConsoleUI.MenuState.Core
{
    public interface IState
    {
        IState RunState();
    }
}
