using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items;
using RPG_wiedzmin_wanna_be.World;

namespace RPG_wiedzmin_wanna_be.Game.ActionHandling
{
    internal abstract class PlayerAction : IPlayerAction
    {

        private IPlayerAction next_handler;
        public IPlayerAction SetNext(IPlayerAction action)
        {
            next_handler = action;
            return action;
        }

        public virtual void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
        {
            if (next_handler != null)
            {
                next_handler.HandleAction(key, player, dungeon);
            }
        }
    }

    internal class ItemPickUpAction : PlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
        {
            if (key == ConsoleKey.E)
            {
                var tile = dungeon.map[player.pos_x, player.pos_y];
                if (tile.items.Count > 0)
                {
                    tile.items[0].PickMe(player);
                    player.inventory.Add(tile.items[0]);
                    tile.items.RemoveAt(0);
                    Logger.PrintLog("Item picked up.");
                }
                else
                {
                    Logger.PrintLog("No items to pick up.");
                }
            }
            else
            {
                base.HandleAction(key, player, dungeon);
            }
        }
    }

    internal class ItemDropAction : PlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
        {
            if (key == ConsoleKey.Q)
            {
                if (player.InInventory)
                {
                    // Drop from inventory
                    if (DropItem(player, dungeon))
                        Logger.PrintLog("Item dropped from inventory");
                    else
                        Logger.PrintLog("No item to drop");
                }
                else
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
            }
            else
            {
                base.HandleAction(key, player, dungeon);
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

            player.RemoveEffect(item);
            return true;
        }
    }

    internal class InventorySwitchAction : PlayerAction
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

    internal class EquipAction : PlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
        {
            if (key == ConsoleKey.F)
            {
                if (player.InInventory)
                {
                    Logger.PrintLog("Equipping item.");
                    TryEuipItem(player);
                }
                else
                {
                    Logger.PrintLog("Unequipping item.");
                    Unequip(player);
                }
            }
            else
            {
                base.HandleAction(key, player, dungeon);
            }
        }

        private void TryEuipItem(Player player)
        {
            if (player.inventory.Count == 0)
            {
                Logger.PrintLog("no items to equip");
                return;
            }

            IItem item = player.inventory[player.inventory_pos];
            int item_count_before = player.inventory.Count;

            item.EquipMe(player);

            if (item_count_before == player.inventory.Count)
            {
                if (!item.IsEquipable)
                {
                    Logger.PrintLog($"{item} cannot be equipped");
                    return;
                }

                if (item.IsTwoHanded)
                {
                    if (player.RightHand != null || player.LeftHand != null)
                    {
                        player.inventory.Add(item);
                        Logger.PrintLog("Cannot equip: Hands are occupied. ");
                        return;
                    }
                    player.RightHand = item;
                    player.LeftHand = item;

                }
                else
                {
                    if (player.InRightHand)
                    {
                        if (player.RightHand == null)
                            player.RightHand = item;
                        else
                        {
                            Logger.PrintLog("Right hand occupied");
                            return;
                        }

                    }
                    else
                    {
                        if (player.LeftHand == null)
                            player.LeftHand = item;
                        else
                        {
                            Logger.PrintLog("left hand occupied");
                            return;
                        }
                    }
                }
                Logger.PrintLog($"{item} equipped");
                player.inventory.Remove(item);
            }
            else
            {
                Logger.PrintLog($"{item} has been drunk");
            }
            if (player.inventory_pos == player.inventory.Count)
            {
                player.inventory_pos--;
            }
            player.ApplyEffect(item);
            //player.inventory.Remove(item);
        }

        private void Unequip(Player player)
        {
            if (player.InRightHand == true)
            {
                if (player.RightHand == null)
                {
                    Logger.PrintLog("right hand is empty");
                    return;
                }
                else
                {
                    player.inventory.Add(player.RightHand);
                    player.RemoveEffect(player.RightHand);
                    if (player.RightHand.IsTwoHanded == true)
                        player.LeftHand = null;
                    player.RightHand = null;
                }
            }
            else
            {
                if (player.LeftHand == null)
                {
                    Logger.PrintLog("left hand is empty");
                    return;
                }
                else
                {
                    player.RemoveEffect(player.LeftHand);
                    player.inventory.Add(player.LeftHand);
                    if (player.LeftHand.IsTwoHanded == true)
                        player.RightHand = null;
                    player.LeftHand = null;
                }
            }
        }

        internal class InventoryNavigationHandler : PlayerAction
        {
            public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
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
                            player.InRightHand = false;
                            Logger.PrintLog("Selected left hand");
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
                            player.InRightHand = true;
                            Logger.PrintLog("Selected right hand");
                        }

                        break;
                    default:
                        base.HandleAction(key, player, dungeon);
                        break;
                }
            }
        }

        public class ExitHandler : PlayerAction
        {
            public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
            {
                if (key == ConsoleKey.Escape)
                {
                    Logger.PrintLog("Exiting game");
                    Environment.Exit(0);
                }
                else
                {
                    base.HandleAction(key, player, dungeon);
                }

            }
        }

        internal class InvalidInputAction : PlayerAction
        {
            public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
            {
                Logger.PrintLog("Invalid input.");
                base.HandleAction(key, player, dungeon);
            }
        }
    }
}
