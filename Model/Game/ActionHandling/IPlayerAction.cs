using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Game.ActionHandling
{
    internal interface IPlayerAction
    {
        IPlayerAction SetNext(IPlayerAction action);
        void HandleAction(ConsoleKey key, Player player, Dungeon dungeon, TurnManager turn_manager);
    }
}
