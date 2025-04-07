using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.World;

namespace RPG_wiedzmin_wanna_be.Game.ActionHandling
{
    internal class InventorySwitchAction : BasePlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
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
                base.HandleAction(key, player, dungeon);
            }
        }
    }
}
