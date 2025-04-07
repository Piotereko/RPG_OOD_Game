using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items;
using RPG_wiedzmin_wanna_be.World;

namespace RPG_wiedzmin_wanna_be.Game.ActionHandling
{
    internal class EquipAction : BasePlayerAction
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
            player.ApplyItemEffect(item);
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
                    player.RemoveItemEffect(player.RightHand);
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
                    player.RemoveItemEffect(player.LeftHand);
                    player.inventory.Add(player.LeftHand);
                    if (player.LeftHand.IsTwoHanded == true)
                        player.RightHand = null;
                    player.LeftHand = null;
                }
            }
        }

        internal class InventoryNavigationHandler : BasePlayerAction
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

        public class ExitHandler : BasePlayerAction
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

        internal class InvalidInputAction : BasePlayerAction
        {
            public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
            {
                Logger.PrintLog("Invalid input.");
                base.HandleAction(key, player, dungeon);
            }
        }
    }
}
