using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.World;

namespace RPG_wiedzmin_wanna_be.Model.Game.ActionHandling
{
    internal class InventorySwitchAction : BasePlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon, TurnManager turn_manager)
        {
            if (key == ConsoleKey.R)
            {
                player.InInventory = !player.InInventory;
                player.inventory_pos = player.InInventory ? 0 : player.inventory_pos;
                if (player.InInventory)
                {
                    Logger.PrintLog("Switched to inventory.");
                }
                else
                {
                    Logger.PrintLog("Switched to hands.");
                }
            }
            else
            {
                base.HandleAction(key, player, dungeon, turn_manager);
            }
        }
    }
}
