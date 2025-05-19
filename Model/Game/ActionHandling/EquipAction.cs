using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.Items;
using RPG_wiedzmin_wanna_be.Model.World;

namespace RPG_wiedzmin_wanna_be.Model.Game.ActionHandling
{
    internal partial class EquipAction : BasePlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon, TurnManager turn_manager)
        {
            if (key == ConsoleKey.F)
            {
                if (player.InInventory)
                {
                    Logger.PrintLog("Equipping item.");
                    TryEuipItem(player, turn_manager);
                }
                else
                {
                    Logger.PrintLog("Unequipping item.");
                    Unequip(player);
                }
            }
            else
            {
                base.HandleAction(key, player, dungeon, turn_manager);
            }
        }

        private void TryEuipItem(Player player, TurnManager turn_manager)
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
                        //player.inventory.Add(item);
                        Logger.PrintLog("Cannot equip: Hands are occupied. ");
                        return;
                    }
                    player.RightHand = item;
                    player.LeftHand = item;

                }
                else
                {
                    /* if (player.InRightHand)
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
                     }*/
                    if (player.RightHandChoosed && player.LeftHandChoosed)
                    {
                        if (player.RightHand == null)
                            player.RightHand = item;
                        else if (player.LeftHand == null)
                            player.LeftHand = item;
                        else
                        {
                            Logger.PrintLog("Right hand occupied");
                            return;
                        }
                    }
                    else if (player.RightHandChoosed)
                    {
                        if (player.RightHand == null)
                            player.RightHand = item;
                        else
                        {
                            Logger.PrintLog("Right hand occupied");
                            return;
                        }
                    }
                    else if (player.LeftHandChoosed)
                    {
                        if (player.LeftHand == null)
                            player.LeftHand = item;
                        else
                        {
                            Logger.PrintLog("left hand occupied");
                            return;
                        }
                    }
                    else
                    {
                        Logger.PrintLog("Please select one hand!");
                        return;
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
            player.ApplyItemEffect(item, turn_manager);
            //player.inventory.Remove(item);
        }

        private void Unequip(Player player)
        {
            if (player.RightHandChoosed && player.RightHand != null)
            {
                player.inventory.Add(player.RightHand);
                player.RemoveItemEffect(player.RightHand);


                if (player.RightHand.IsTwoHanded)
                {
                    player.LeftHand = null;
                }

                player.RightHand = null;
                Logger.PrintLog("Right hand unequipped.");
            }

            else if (player.LeftHandChoosed && player.LeftHand != null)
            {
                player.RemoveItemEffect(player.LeftHand);
                player.inventory.Add(player.LeftHand);

                if (player.LeftHand.IsTwoHanded)
                {
                    player.RightHand = null;
                }

                player.LeftHand = null;
                Logger.PrintLog("Left hand unequipped.");
            }
            else
            {
                Logger.PrintLog("No item chosen to unequip.");
            }
        }
    }
}
