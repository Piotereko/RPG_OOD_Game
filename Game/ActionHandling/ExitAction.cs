using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.World;

namespace RPG_wiedzmin_wanna_be.Game.ActionHandling
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
                base.HandleAction(key, player, dungeon,turn_manager);
            }

        }
    }

}
