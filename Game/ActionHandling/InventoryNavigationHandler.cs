using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.World;

namespace RPG_wiedzmin_wanna_be.Game.ActionHandling
{

    internal class InventoryNavigationHandler : BasePlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon, TurnManager turn_manager)
        {

            switch (key)
            {
                case ConsoleKey.D1:
                    if (player.InInventory)
                    {
                        if (player.inventory.Count > 0)
                        {
                            player.inventory_pos = (player.inventory_pos - 1 + player.inventory.Count) % player.inventory.Count;
                            Logger.PrintLog($"Selected item {player.inventory_pos + 1}/{player.inventory.Count}");
                        }
                    }
                    else
                    {
                        if (player.LeftHandChoosed)
                        {
                            player.LeftHandChoosed = false;
                            Logger.PrintLog("Unselected left hand");
                        }
                        else
                        {
                            player.LeftHandChoosed = true;
                            Logger.PrintLog("Selected left hand");
                        }
                    }
                    break;
                case ConsoleKey.D2:
                    if (player.InInventory)
                    {
                        if (player.inventory.Count > 0)
                        {
                            player.inventory_pos = (player.inventory_pos + 1) % player.inventory.Count;
                            Logger.PrintLog($"Selected item {player.inventory_pos + 1}/{player.inventory.Count}");
                        }
                    }
                    else
                    {   
                        if (player.RightHandChoosed)
                        {
                            player.RightHandChoosed = false;
                            Logger.PrintLog("Unselected right hand");
                        }
                        else
                        {
                            player.RightHandChoosed = true;
                            Logger.PrintLog("Selected right hand");
                        }
                    }

                    break;
                default:
                    base.HandleAction(key, player, dungeon, turn_manager);
                    break;
            }
        }
    }

}
