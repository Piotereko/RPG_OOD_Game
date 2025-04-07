using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items;
using RPG_wiedzmin_wanna_be.World;

namespace RPG_wiedzmin_wanna_be.Game.ActionHandling
{
    internal class ItemDropAction : BasePlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
        {

            if (player.InInventory)
            {
                if (key == ConsoleKey.Q)
                {
                    // Drop from inventory
                    if (DropItem(player, dungeon))
                        Logger.PrintLog("Item dropped from inventory");
                    else
                        Logger.PrintLog("No item to drop");
                }
                else if (key == ConsoleKey.Z)
                {
                    int droped = DropAllInventoryItems(player, dungeon);
                    Logger.PrintLog($"Dropped {droped} items from inventory");
                }
                else
                {
                    base.HandleAction(key, player, dungeon);
                }
            }
            else
            {
                if (key == ConsoleKey.Q)
                {
                    // Drop from hands
                    if (DropEquippedItem(player, dungeon))
                    {
                        Logger.PrintLog("Item dropped from hand");
                    }
                    else
                    {
                        Logger.PrintLog("No item in hand to drop");
                    }
                }
                else
                {
                    base.HandleAction(key, player, dungeon);
                }
            }

        }
        private bool DropItem(Player player, Dungeon dungeon)
        {
            if (player.InInventory == true)
            {
                if (player.inventory.Count > 0)
                {
                    Tile tile = dungeon.map[player.pos_x, player.pos_y];
                    IItem item = player.inventory[player.inventory_pos]; //item to drop

                    item.X_position = player.pos_x; item.Y_position = player.pos_y;
                    item.DropMe(player);
                    tile.items.Add(item);
                    player.inventory.RemoveAt(player.inventory_pos);
                    if (player.inventory_pos == player.inventory.Count) //if we drop last item from inventowy we need to go to previous one
                        player.inventory_pos--;

                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        private bool DropEquippedItem(Player player, Dungeon dungeon)
        {
            IItem? item;
            if (player.InRightHand)
            {
                item = player.RightHand;
            }
            else
            {
                item = player.LeftHand;
            }
            if (item == null) return false;

            Tile tile = dungeon.map[player.pos_x, player.pos_y];
            item.X_position = player.pos_x;
            item.Y_position = player.pos_y;
            item.DropMe(player);

            tile.items.Add(item);

            if (player.InRightHand)
            {
                player.RightHand = null;
                if (item.IsTwoHanded) player.LeftHand = null;
            }
            else
            {
                player.LeftHand = null;
                if (item.IsTwoHanded) player.RightHand = null;
            }

            player.RemoveItemEffect(item);
            return true;
        }

        private int DropAllInventoryItems(Player player, Dungeon dungeon)
        {
            if (player.inventory.Count == 0)
            {
                return 0;
            }

            int droppedCount = 0;
            while (player.inventory.Count > 0)
            {
                if (DropItem(player, dungeon))
                {
                    droppedCount++;
                }
                else
                {
                    break;
                }
            }
            player.inventory_pos = 0;
            return droppedCount;
        }
    }
}
