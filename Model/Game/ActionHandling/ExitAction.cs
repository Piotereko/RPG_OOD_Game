using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.World;

namespace RPG_wiedzmin_wanna_be.Model.Game.ActionHandling
{

    internal class ExitHandler : BasePlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon, TurnManager turn_manager)
        {
            if (key == ConsoleKey.Escape)
            {
                Logger.PrintLog("Exiting game");
                Environment.Exit(0);
            }
            else
            {
                base.HandleAction(key, player, dungeon, turn_manager);
            }

        }
    }

}
