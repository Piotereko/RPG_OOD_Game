using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items;
using RPG_wiedzmin_wanna_be.World;

namespace RPG_wiedzmin_wanna_be.Game
{
    internal sealed class Render
    {
        private static Render instance;

        private Render() { }

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
            for (int i = 0; i < _world.height; i++)
            {
                for (int j = 0; j < _world.width; j++)
                {
                    if (_world.map[j, i].IsWall)
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
                Tile tile = world.map[item.X_position, item.Y_position];
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
            foreach (Enemy enemy in world.enemies)
            {
                Console.SetCursorPosition(enemy.pos_x, enemy.pos_y);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{enemy.EntitySing()}");
                Console.ResetColor();
            }
        }

        public void printEnemiesInfo(Dungeon world,Player player)
        {
            Console.SetCursorPosition(96, 11);
            Console.Write("#####################");
            Console.SetCursorPosition(96, 12);
            Console.Write("Enemies nearby:");
            var enemiesWithDistances = world.enemies
            .Select(enemy => new
            {
                Enemy = enemy,
                Distance = Math.Sqrt(Math.Pow(player.pos_x - enemy.pos_x, 2) + Math.Pow(player.pos_y - enemy.pos_y, 2))
            })
            .OrderBy(enemyWithDistance => enemyWithDistance.Distance).ToList();

            ClearArea(96, 13, 20, 10);

            int index = 0;
            foreach (var enemyWithDistance in enemiesWithDistances)
            {
                Console.SetCursorPosition(96, 13 + index++);
                Console.WriteLine($"{enemyWithDistance.Enemy.GetType().Name} ({enemyWithDistance.Distance:F0})");
            }

        }

        public void PrintStats(Player player)
        {

            ClearArea(41, 0, 20, 15);
            Console.SetCursorPosition(41, 0);
            Console.Write("################################");
            Console.SetCursorPosition(41, 1);
            Console.WriteLine("Player Statistic:");
            Console.SetCursorPosition(41, 2);
            Console.WriteLine("Health:    " + player.health);
            Console.SetCursorPosition(41, 3);
            Console.WriteLine("Strength:  " + player.strength);
            Console.SetCursorPosition(41, 4);
            Console.WriteLine("Dexterity: " + player.dexterity);
            Console.SetCursorPosition(41, 5);
            Console.WriteLine("Luck:      " + player.luck);
            Console.SetCursorPosition(41, 6);
            Console.WriteLine("Agression: " + player.agression);
            Console.SetCursorPosition(41, 7);
            Console.WriteLine("Wisdom:    " + player.wisdom);
            Console.SetCursorPosition(41, 8);
            Console.WriteLine("Coins:     " + player.coins_amount);
            Console.SetCursorPosition(41, 9);
            Console.WriteLine("Gold:      " + player.gold_amount);
        }



        public void PrintInventory(Player player)
        {
            Console.SetCursorPosition(41, 11);
            Console.Write("#####################");

            ClearArea(41, 12, 30, player.inventory.Count + 2);

            Console.SetCursorPosition(41, 12);

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
                Console.SetCursorPosition(41, 13 + index++);
                Console.Write(item);
                Console.ResetColor();
            }
        }


        public void PrintTileInfo(Tile tile)
        {
            Console.SetCursorPosition(68, 11);
            Console.Write("#####################");
            Console.SetCursorPosition(68, 12);
            Console.WriteLine("Current tile:");

            ClearArea(68, 13, 30, 15);

            int index = 0;
            if (tile.items != null)
            {
                foreach (IItem item in tile.items)
                {
                    Console.SetCursorPosition(68, 13 + index++);
                    Console.Write(item.ToString());
                }
            }
        }



        public void PrintSteering(Dungeon dungeon, Player player)
        {
            /*Console.SetCursorPosition(80, 0);
            Console.Write("##################################");
            Console.SetCursorPosition(80, 1);
            Console.Write("Steering:");
            Console.SetCursorPosition(80, 2);
            Console.Write("W,A,S,D - movement");
            Console.SetCursorPosition(80, 3);
            Console.Write("E - pickup item");
            Console.SetCursorPosition(80, 4);
            Console.Write("F - equip");
            Console.SetCursorPosition(80, 5);
            Console.Write("Q - inventory");
            Console.SetCursorPosition(80, 6);
            Console.Write("R - swap hands");*/

            InstructionBuilder instructionBuilder = new InstructionBuilder();

            instructionBuilder.AddMovement();


            if (dungeon.HasWeapons || dungeon.HasPotions)
            {
                instructionBuilder.AddEquipInstruction(dungeon.HasWeapons, dungeon.HasPotions);
            }

            if (dungeon.items.Count > 0 || player.inventory.Count > 0)
            {
                instructionBuilder.AddPickupItemInstruction(dungeon.items.Count > 0, player.inventory.Count > 0);
            }

            instructionBuilder.AddInventoryInstruction()
                              .AddSwapHandsInstruction();

            List<string> instructions = instructionBuilder.Build();

            ClearArea(80, 0, 40, 10);

            Console.SetCursorPosition(80, 0);
            Console.Write("##################################");
            Console.SetCursorPosition(80, 1);
            Console.Write("Steering:");

            int line = 2;
            foreach (var instruction in instructions)
            {
                Console.SetCursorPosition(80, line++);
                Console.Write(instruction);
            }
        }



        public void PrintHands(Player player)
        {
            ClearArea(0, 20, 40, 2);

            Console.SetCursorPosition(0, 20);

            if (player.InRightHand == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write("Left Hand: " + player.LeftHand);
            Console.ResetColor();
            Console.SetCursorPosition(0, 21);
            if (player.InRightHand == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write("Right Hand: " + player.RightHand);
            Console.ResetColor();
        }

        public void PrintPlayer(Player player)
        {
            Console.SetCursorPosition(player.pos_x, player.pos_y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("@");
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
    }
}
