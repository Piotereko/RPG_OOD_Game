using RPG_wiedzmin_wanna_be.DTO.Commands;
using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Entity.Strategy;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.Items;
using RPG_wiedzmin_wanna_be.Model.Items.Weapons;
using RPG_wiedzmin_wanna_be.Model.World;
using RPG_wiedzmin_wanna_be.View;
using RPG_wiedzmin_wanna_be.Visitor_pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model
{
    public class ActionExecutor
    {
        public void Execute(PlayerCommand command, Player player, Dungeon dungeon, TurnManager turnManager)
        {
            switch (command.Type)
            {
                case "move":
                    HandleMoveCommand(command , player, dungeon);
                    break;
                case "equip":
                    HandleEquipCommand(player, turnManager);
                    break;
                case "attack":
                    HandleAttackCommand(player, dungeon);
                    break;
                case "pickup":
                    HandlePickUpCommand(player, dungeon);                                                                                                                                                                                                                                                                                                                                                           
                    break;
                case "drop":
                    HandleDropCommand(command as DropCommand, player, dungeon);
                    break;
                case "exit":
                    System.Environment.Exit(0);
                    break;
                case "inventorynavigate":
                    HandleInventoryNavigateCommand(command , player);
                    break;                          
                case "inventoryswitch":
                    HandleInventorySwitchCommand(player);
                    break;
                case "attackmodeswitch":
                    HandleAttackModeSwitchCommand(player);
                    break;
                default:
                    Logger.PrintLog("Unknown command type: " + command.Type);
                    break;
            }
        }

        private void HandleMoveCommand(PlayerCommand command, Player player, Dungeon dungeon)
        {
            

            if (!command.Parameters.TryGetValue("direction", out string? direction))
            {
                Logger.PrintLog("Move command missing 'direction' parameter.");
                return;
            }

            int newX = player.pos_x;
            int newY = player.pos_y;

            switch (direction.ToLower())
            {
                case "up":
                    newY -= 1;
                    break;
                case "down":
                    newY += 1;
                    break;
                case "left":
                    newX -= 1;
                    break;
                case "right":
                    newX += 1;
                    break;
                default:
                    Logger.PrintLog($"Unknown move direction: {direction}");
                    return;
            }

            TryWalk(newX, newY, player, dungeon);
        }

        private void TryWalk(int newX, int newY, Player player, Dungeon dungeon)
        {
            if (!dungeon.map[newX][ newY].IsWall)
            {
                Render.Instance.printTile(dungeon.map[player.pos_x][ player.pos_y]);
                player.pos_x = newX;
                player.pos_y = newY;
                Render.Instance.PrintPlayer(player);
            }
            else
            {
                Logger.PrintLog("Unable to move: Wall");
            }
        }

        private void HandleEquipCommand(Player player, TurnManager turnManager)
        {
  
                if (player.InInventory) 
                {
                    TryEquipItem(player, turnManager);
                }
                else
                {
                    Unequip(player);
                }
        }

        private void TryEquipItem(Player player, TurnManager turnManager)
        {
            if (player.inventory.Count == 0)
            {
                Logger.PrintLog("No items to equip");
                return;
            }

            IItem item = player.inventory[player.inventory_pos];
            int itemCountBefore = player.inventory.Count;

            item.EquipMe(player);

            if (itemCountBefore == player.inventory.Count)
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
                        Logger.PrintLog("Cannot equip: Hands are occupied.");
                        return;
                    }
                    player.RightHand = item;
                    player.LeftHand = item;
                }
                else
                {
                    if (player.RightHandChoosed && player.LeftHandChoosed)
                    {
                        if (player.RightHand == null)
                            player.RightHand = item;
                        else if (player.LeftHand == null)
                            player.LeftHand = item;
                        else
                        {
                            Logger.PrintLog("Hands are occupied");
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
                            Logger.PrintLog("Left hand occupied");
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
                Logger.PrintLog($"{item} has been consumed");
            }

            if (player.inventory_pos == player.inventory.Count)
            {
                player.inventory_pos--;
            }
            player.ApplyItemEffect(item, turnManager);
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

        private void HandleAttackCommand(Player player, Dungeon dungeon)
        {
            var nearbyEnemies = dungeon.enemies
                .Where(e => (Math.Abs(e.pos_x - player.pos_x) <= 1 && Math.Abs(e.pos_y - player.pos_y) <= 1) && e.IsAlive)
                .ToList();

            if (!nearbyEnemies.Any())
            {
                Logger.PrintLog("No enemies nearby to attack!");
                return;
            }

            var enemy = nearbyEnemies.First();
            FightEnemy(player, enemy);
        }

        private void FightEnemy(Player player, Enemy enemy)
        {
            if (enemy == null || !enemy.IsAlive) return;

            IAttackVisitor attackVisitor = player.attack_mode switch
            {
                0 => new NormalAttackVisitor(),
                1 => new StealthAttackVisitor(),
                2 => new MagicAttackVisitor(),
                _ => new NormalAttackVisitor()
            };

/*            IDefenseVisitor defenseVisitor = player.attack_mode switch
            {
                0 => new NormalDefenseVisitor(),
                1 => new StealthDefenseVisitor(),
                2 => new MagicDefenseVisitor(),
                _ => new NormalDefenseVisitor()
            };*/

            int rightDamage = 0;
            if (player.RightHandChoosed && player.RightHand != null)
            {
                rightDamage = ((Weapon)player.RightHand).AcceptAttack(attackVisitor, player);
            }

            int leftDamage = 0;
            if (player.LeftHandChoosed && player.LeftHand != null)
            {
                leftDamage = ((Weapon)player.LeftHand).AcceptAttack(attackVisitor, player);
            }

            int totalDamage = leftDamage + rightDamage;

            if (player.LeftHandChoosed && player.RightHandChoosed && player.RightHand != null && player.RightHand.IsTwoHanded)
            {
                totalDamage /= 2;
            }

            enemy.TakeDamage(totalDamage);
            Logger.PrintLog($"You attacked {enemy.GetType().Name} for {totalDamage} damage!");

            if (enemy.Behavior.GetType() != typeof(AgressiveBehaviour))
            {
                enemy.Behavior = new AgressiveBehaviour();
                Logger.PrintLog($" {enemy.GetType().Name} is now agressive! ");
            }


          

            if (enemy.health <= 0)
            {
                Logger.PrintLog($"You defeated {enemy.GetType().Name}!");
                return;
            }

            if (enemy.health < 5 && enemy.Behavior.GetType() != typeof(SkittishBehaviour))
            {
                enemy.Behavior = new SkittishBehaviour();
                Logger.PrintLog($" {enemy.GetType().Name} is running away! ");
            }

            /* int leftDefense = 0;
             if (player.LeftHandChoosed && player.LeftHand != null)
             {
                 leftDefense = ((Weapon)player.LeftHand).AcceptDeffence(defenseVisitor, player);
             }

             int rightDefense = 0;
             if (player.RightHandChoosed && player.RightHand != null)
             {
                 rightDefense = ((Weapon)player.RightHand).AcceptDeffence(defenseVisitor, player);
             }

             int totalDefense = leftDefense + rightDefense;*/

            /* int enemyDamage = Math.Max(1, enemy.attack_val - totalDefense);
             player.health -= enemyDamage;
             Logger.PrintLog($"{enemy.GetType().Name} counterattacked for {enemyDamage} damage!");

             if (player.health <= 0)
             {
                 Logger.PrintLog("You have been defeated!");
                 Environment.Exit(0);
             }*/
        }
        private void HandlePickUpCommand(Player player, Dungeon dungeon)
        {
            var tile = dungeon.map[player.pos_x][player.pos_y];

            if (tile.items.Count > 0)
            {
                var item = tile.items[0];
                item.PickMe(player);
                player.inventory.Add(item);
                tile.items.RemoveAt(0);
                Logger.PrintLog("Item picked up.");
            }
            else
            {
                Logger.PrintLog("No items to pick up.");
            }
        }

        private void HandleDropCommand(DropCommand? dropCmd, Player player, Dungeon dungeon)
        {
            if (dropCmd == null)
            {
                Logger.PrintLog("Drop command is null.");
                return;
            }

            if (!dropCmd.Parameters.TryGetValue("mode", out string? mode))
            {
                Logger.PrintLog("Drop command missing 'mode' parameter.");
                return;
            }

            switch (mode)
            {
                case "single":
                    if (player.InInventory)
                    {
                        if (DropItem(player, dungeon))
                            Logger.PrintLog("Item dropped from inventory");
                        else
                            Logger.PrintLog("No item to drop");
                    }
                    else
                    {
                        if (DropEquippedItem(player, dungeon))
                            Logger.PrintLog("Item dropped from hand");
                        else
                            Logger.PrintLog("No item in hand to drop");
                    }
                    break;

                case "all":
                    int droppedCount = DropAllInventoryItems(player, dungeon);
                    Logger.PrintLog($"Dropped {droppedCount} items from inventory");
                    break;

                default:
                    Logger.PrintLog($"Unknown drop mode: {mode}");
                    break;
            }
        }

        private bool DropItem(Player player, Dungeon dungeon)
        {
            if (player.InInventory && player.inventory.Count > 0)
            {
                Tile tile = dungeon.map[player.pos_x][player.pos_y];
                IItem item = player.inventory[player.inventory_pos];

                item.X_position = player.pos_x;
                item.Y_position = player.pos_y;
                item.DropMe(player);
                tile.items.Add(item);
                player.inventory.RemoveAt(player.inventory_pos);

                if (player.inventory_pos == player.inventory.Count)
                    player.inventory_pos--;

                return true;
            }
            return false;
        }

        private bool DropEquippedItem(Player player, Dungeon dungeon)
        {
            IItem? item = null;

            if (player.RightHandChoosed && player.RightHand != null)
            {
                item = player.RightHand;
            }
            else if (player.LeftHandChoosed && player.LeftHand != null)
            {
                item = player.LeftHand;
            }

            if (item == null) return false;

            Tile tile = dungeon.map[player.pos_x][  player.pos_y];
            item.X_position = player.pos_x;
            item.Y_position = player.pos_y;
            item.DropMe(player);

            tile.items.Add(item);

            if (player.RightHandChoosed && player.RightHand != null)
            {
                player.RightHand = null;
                if (item.IsTwoHanded)
                    player.LeftHand = null;
            }
            else if (player.LeftHandChoosed && player.LeftHand != null)
            {
                player.LeftHand = null;
                if (item.IsTwoHanded)
                    player.RightHand = null;
            }

            player.RemoveItemEffect(item);

            return true;
        }

        private int DropAllInventoryItems(Player player, Dungeon dungeon)
        {
            if (player.inventory.Count == 0)
                return 0;

            int droppedCount = 0;
            while (player.inventory.Count > 0)
            {
                if (DropItem(player, dungeon))
                    droppedCount++;
                else
                    break;
            }
            player.inventory_pos = 0;
            return droppedCount;
        }

        private void HandleInventoryNavigateCommand(PlayerCommand command, Player player)
        {
            if (command == null || !command.Parameters.TryGetValue("target", out string? target))
            {
                Logger.PrintLog("InventoryNavigate command missing 'direction' parameter.");
                return;
            }

            switch (target.ToLower())
            {
                case "left":  // Corresponds to ConsoleKey.D1
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
                        player.LeftHandChoosed = !player.LeftHandChoosed;
                        Logger.PrintLog(player.LeftHandChoosed ? "Selected left hand" : "Unselected left hand");
                    }
                    break;

                case "right":  // Corresponds to ConsoleKey.D2
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
                        player.RightHandChoosed = !player.RightHandChoosed;
                        Logger.PrintLog(player.RightHandChoosed ? "Selected right hand" : "Unselected right hand");
                    }
                    break;

                default:
                    Logger.PrintLog($"Unknown inventory navigation direction: {target}");
                    break;
            }
        }

        private void HandleInventorySwitchCommand(Player player)
        {
            player.InInventory = !player.InInventory;
            if (player.InInventory)
            {
                player.inventory_pos = 0;
                Logger.PrintLog("Switched to inventory.");
            }
            else
            {
                Logger.PrintLog("Switched to hands.");
            }
        }

        private void HandleAttackModeSwitchCommand(Player player)
        {
            Logger.PrintLog("Attack mode changed");
            player.attack_mode = (player.attack_mode + 1) % 3;
        }

    }
}

