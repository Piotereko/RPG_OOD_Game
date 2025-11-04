using RPG_wiedzmin_wanna_be.Model.Items.Potions;
using RPG_wiedzmin_wanna_be.Model.Effects;
using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Items;
using RPG_wiedzmin_wanna_be.Model.World;
using System;
using System.Text;
using RPG_wiedzmin_wanna_be.Model.Game;
using System.Numerics;

namespace RPG_wiedzmin_wanna_be.View
{
    internal sealed class Render
    {
        private static Render? instance;

        private Render()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public static Render Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Render();
                }
                return instance;
            }
        }
        public void ClearArea(int x, int y, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(new string(' ', width));
            }
        }


        public void printTile(Tile tile)
        {
            Console.SetCursorPosition(tile.pos_x, tile.pos_y);
            if (tile.IsWall)
            {
                Console.Write("█");
            }
            else if (tile.items.Count > 1)
            {
                Console.Write("M");
            }
            else if (tile.items.Count > 0)
            {
                printItem(tile.items[0]);
            }
            else
            {
                Console.Write(" ");
            }
        }

        public void printItem(IItem item)
        {
            switch (item.ItemSign())
            {
                case '$':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("$");
                    break;
                case 'P':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("P");
                    break;
                case 'W':
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("W");
                    break;
                case 'R':
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("R");
                    break;
                case 'O':
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("W");
                    break;
            }
            //Console.Write(item.GetType().ToString()[28]);
            Console.ResetColor();
        }

        public void printWorld(Dungeon _world)
        {
            Console.Clear();
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < _world.height; i++)
            {
                for (int j = 0; j < _world.width; j++)
                {
                    if (_world.map[j][i].IsWall)
                    {
                        Console.Write("█");
                    }
                    else
                        Console.Write(" ");

                }
                Console.WriteLine();
            }
        }



        public void printItems(Dungeon world)
        {
            foreach (IItem item in world.items)
            {
                Console.SetCursorPosition(item.X_position, item.Y_position);
                Tile tile = world.map[item.X_position][item.Y_position];
                if (tile.items.Count > 1)
                {
                    Console.Write("M");
                }
                else if (tile.items.Count > 0)
                {
                    printItem(item);
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }

        public void printEnemies(Dungeon world)
        {


            for (int i = 0; i < world.enemies.Count; i++)
            {
                Enemy enemy = world.enemies[i];
                printEnemy(enemy);
            }
        }

        public void printEnemy(Enemy enemy)
        {
            Console.SetCursorPosition(enemy.pos_x, enemy.pos_y);
            if (enemy.IsAlive)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{enemy.EntitySing()}");
                Console.ResetColor();
            }
            else
            {
                Console.Write(' ');
            }
        }

        public void printEnemiesInfo(Dungeon world, Player player)
        {
            Console.SetCursorPosition(96, 13);
            Console.Write("#####################");
            Console.SetCursorPosition(96, 14);
            Console.Write("Enemies nearby:");
            var enemiesWithDistances = world.enemies
            .Select(enemy => new
            {
                Enemy = enemy,
                Distance = Math.Sqrt(Math.Pow(player.pos_x - enemy.pos_x, 2) + Math.Pow(player.pos_y - enemy.pos_y, 2))
            })
            .OrderBy(enemyWithDistance => enemyWithDistance.Distance).Take(13).ToList();

            ClearArea(96, 15, 20, 10);

            int index = 0;
            foreach (var enemyWithDistance in enemiesWithDistances)
            {
                if (enemyWithDistance.Enemy.IsAlive)
                {
                    Console.SetCursorPosition(96, 15 + index++);
                    Console.WriteLine($"{enemyWithDistance.Enemy.GetType().Name} ({enemyWithDistance.Distance:F0}) {enemyWithDistance.Enemy.Behavior.GetType().Name[0]} HP: {enemyWithDistance.Enemy.health}");
                }
            }

        }

        public void PrintStats(Player player)
        {
            ClearArea(41, 0, 26, 15);
            Console.SetCursorPosition(41, 0);
            Console.Write("#####################");
            Console.SetCursorPosition(41, 1);
            Console.WriteLine("Player Statistic:");
            Console.SetCursorPosition(41, 2);
            Console.WriteLine("Health:     " + player.health);
            Console.SetCursorPosition(41, 3);
            Console.WriteLine("Strength:   " + player.strength);
            Console.SetCursorPosition(41, 4);
            Console.WriteLine("Dexterity:  " + player.dexterity);
            Console.SetCursorPosition(41, 5);
            Console.WriteLine("Luck:       " + player.luck);
            Console.SetCursorPosition(41, 6);
            Console.WriteLine("Agression:  " + player.agression);
            Console.SetCursorPosition(41, 7);
            Console.WriteLine("Wisdom:     " + player.wisdom);
            Console.SetCursorPosition(41, 8);
            Console.WriteLine("Coins:      " + player.coins_amount);
            Console.SetCursorPosition(41, 9);
            Console.WriteLine("Gold:       " + player.gold_amount);
            Console.SetCursorPosition(41, 10);
            Console.Write("Attack mode:");
            switch (player.attack_mode)
            {
                case 0:
                    Console.WriteLine("Normal attack");
                    break;
                case 1:
                    Console.WriteLine("Stealth attack");
                    break;
                case 2:
                    Console.WriteLine("Magic attack");
                    break;
            }
        }



        public void PrintInventory(Player player)
        {
            Console.SetCursorPosition(41, 13);
            Console.Write("#####################");

            ClearArea(41, 14, 30, 16);

            Console.SetCursorPosition(41, 14);

            if (player.InInventory == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("Player Inventory:");
            Console.ResetColor();
            int index = 0;
            for (int i = 0; i < player.inventory.Count; i++)
            {
                IItem item = player.inventory[i];
                if (player.inventory_pos == i && player.InInventory == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.SetCursorPosition(41, 15 + index++);
                Console.Write(item);
                Console.ResetColor();
            }
        }


        public void PrintTileInfo(Tile tile)
        {
            Console.SetCursorPosition(68, 13);
            Console.Write("#####################");
            Console.SetCursorPosition(68, 14);
            Console.WriteLine("Current tile:");

            ClearArea(68, 15, 30, 15);

            int index = 0;
            if (tile.items != null)
            {
                foreach (IItem item in tile.items)
                {
                    Console.SetCursorPosition(68, 15 + index++);
                    Console.Write(item.ToString());
                }
            }
        }



        public void PrintSteering(Dungeon dungeon, Player player)
        {
            InstructionBuilder instructionBuilder = new InstructionBuilder();

            instructionBuilder.AddMovement().AddModeSwitch();

            if (dungeon.items.Count > 0)
            {
                instructionBuilder.AddPickupItemInstruction();
            }
            if (player.inventory.Count > 0)
            {
                instructionBuilder.AddDropInstruction(player.InInventory);
            }

            if (dungeon.HasWeapons || dungeon.HasPotions)
            {
                bool has_hand_item = false;
                if (player.InRightHand && player.RightHand != null)
                {
                    has_hand_item = true;
                }
                else if (!player.InRightHand && player.LeftHand != null)
                {
                    has_hand_item = true;
                }
                instructionBuilder.AddEquipInstruction(dungeon.HasWeapons, dungeon.HasPotions, player.InInventory, has_hand_item);
            }

            bool enemiesNearby = dungeon.enemies.Any(e => Math.Abs(e.pos_x - player.pos_x) <= 1 && Math.Abs(e.pos_y - player.pos_y) <= 1);

            if (enemiesNearby)
            {
                instructionBuilder.AddAttackInstruction();
            }

            instructionBuilder.AddInventoryMoveing(player.inventory.Count > 0, player.InInventory)
                               .AddAttackMode()
                              .AddExitInstruction();

            List<string> instructions = instructionBuilder.Build();

            ClearArea(68, 0, 40, 12);

            Console.SetCursorPosition(68, 0);
            Console.Write("#####################");
            Console.SetCursorPosition(68, 1);
            Console.Write("Steering:");

            int line = 2;
            foreach (var instruction in instructions)
            {
                Console.SetCursorPosition(68, line++);
                Console.Write(instruction);
            }
        }



        public void PrintHands(Player player)
        {
            ClearArea(0, 20, 40, 2);

            Console.SetCursorPosition(0, 20);

            if (!player.InInventory && player.LeftHandChoosed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            if (player.LeftHandChoosed)
            {
                Console.Write("->");
            }
            Console.Write("Left Hand: " + player.LeftHand);
            Console.ResetColor();
            Console.SetCursorPosition(0, 21);
            if (!player.InInventory && player.RightHandChoosed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            if (player.RightHandChoosed)
            {
                Console.Write("->");
            }
            Console.Write("Right Hand: " + player.RightHand);
            Console.ResetColor();
        }

        public void PrintPlayer(Player player)
        {
            Console.SetCursorPosition(player.pos_x, player.pos_y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("¶");
            Console.ResetColor();
        }

        public void PrintLogs()
        {
            int logStartY = 24;
            Console.SetCursorPosition(0, logStartY - 1);
            Console.WriteLine("Logs:");

            ClearArea(0, logStartY, 40, 5);

            int index = 0;
            foreach (var log in Logger.GetLogs())
            {
                Console.SetCursorPosition(0, logStartY + index);
                Console.WriteLine(log);
                index++;
            }
        }

        public void PrintActiveEffects(TurnManager turnManager)
        {
            Console.SetCursorPosition(96, 0);
            Console.Write("#####################");
            Console.SetCursorPosition(96, 1);
            Console.Write("Active Effects:");


            ClearArea(96, 2, 17, 9);

            int index = 0;

            foreach (IEffect effect in turnManager.effects)
            {
                Console.SetCursorPosition(96, 2 + index++);
                Console.Write($"{effect.Name}({effect.Duration})");
            }
        }

        public void PrintPlayers(Dictionary<int,Player> players,int local_id)
        {
            if (local_id == -1)
            {
                foreach (var player in players.Values)
                {
                    Console.SetCursorPosition(player.pos_x, player.pos_y);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{player.player_id}");
                    Console.ResetColor();
                }
            }
            else
            {
                foreach(var kvp in players)
                {
                    Player pl = kvp.Value;
                    if(local_id == kvp.Key)
                    {
                        Console.SetCursorPosition(pl.pos_x, pl.pos_y);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{pl.player_id}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.SetCursorPosition(pl.pos_x, pl.pos_y);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"{pl.player_id}");
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}
