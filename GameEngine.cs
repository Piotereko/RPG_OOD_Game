using RPG_wiedzmin_wanna_be.Items;

namespace RPG_wiedzmin_wanna_be
{
    internal class GameEngine
    {
        World world = new World();
        Player player = new Player();

        private Queue<string> logMessages = new Queue<string>(); //stores logs
        private const int logLimit = 5;
        private const int logStartY = 24;

        public void printTile(int x, int y)
        {
            Tile tile = world.map[x, y];
            Console.SetCursorPosition(x, y);
            if (tile.IsWall)
            {
                Console.Write("█");
            }
            else if (tile.items.Count > 0)
            {
                Console.Write("I");
            }
            else
            {
                Console.Write(" ");
            }
        }
        public void printWorld(World _world)
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
        public void printItems()
        {
            foreach (IItem item in world.items)
            {
                Console.SetCursorPosition(item.X_position, item.Y_position);
                Console.Write("I");
            }
        }

        public void printLog(string msg)
        {
            if (logMessages.Count >= logLimit)
            {
                logMessages.Dequeue();
            }
            logMessages.Enqueue(msg);
            for (int i = 0; i < logLimit; i++)
            {
                Console.SetCursorPosition(0, logStartY + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            int index = 0;
            foreach (string log in logMessages)
            {
                Console.SetCursorPosition(0, logStartY + index);
                Console.WriteLine(log);
                index++;
            }
        }

        public void PrintStats()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(41, i);
                Console.Write("                                       ");
            }
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

            //Console.SetCursorPosition(0, 0);
        }

        public void PrintInventory()
        {
            Console.SetCursorPosition(41, 11);
            Console.Write("################################");


            for (int i = 0; i < player.inventory.Count + 2; i++)
            {
                Console.SetCursorPosition(41, 12 + i);
                Console.Write("                                       ");
            }
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

        public void PrintTileInfo()
        {
            Console.SetCursorPosition(80, 11);
            Console.Write("################################");
            Console.SetCursorPosition(80, 12);
            Console.WriteLine("Current tile:");
            int index = 0;
            Tile current_tile = world.map[player.pos_x, player.pos_y];
            for (int i = 0; i < logLimit; i++)
            {
                Console.SetCursorPosition(80, 13 + i);
                Console.Write("                                       ");
            }
            if (current_tile.items != null)
            {
                foreach (IItem item in current_tile.items)
                {
                    Console.SetCursorPosition(80, 13 + index++);
                    Console.Write(item.ToString());
                }
            }

        }

        public bool TryPickUp()
        {
            Tile tile = world.map[player.pos_x, player.pos_y];
            if (tile.items != null && tile.items.Count > 0)
            {
                tile.items[0].PickMe(player);
                player.inventory.Add(tile.items[0]);
                tile.items.RemoveAt(0);
                return true;
            }
            return false;
        }

        public bool DropItem()
        {
            if (player.InInventory == true)
            {
                if (player.inventory.Count > 0)
                {
                    Tile tile = world.map[player.pos_x, player.pos_y];

                    player.inventory[player.inventory_pos].DropMe(player);

                    tile.items.Add(player.inventory[player.inventory_pos]);
                    player.inventory.RemoveAt(player.inventory_pos);
                    return true;
                }
                else
                    printLog("no item to drop");
            }
            return false;
        }

        public void TryEuipItem()
        {
            if (player.inventory.Count == 0)
            {
                printLog("no items to equip");
                return;
            }
            IItem item = player.inventory[player.inventory_pos];
            if (!item.IsEquipable)
            {
                printLog($"{item} cannot be equipped");
                return;
            }
            if (item.IsTwoHanded)
            {
                if (player.RightHand != null || player.LeftHand != null)
                {
                    printLog("one hand is occupied");
                    return;
                }
                else
                {
                    player.RightHand = item;
                    player.LeftHand = item;
                }
            }
            else
            {
                if (player.InRightHand)
                {
                    if (player.RightHand == null)
                        player.RightHand = item;
                    else
                    {
                        printLog("right hand occupied");
                        return;
                    }

                }
                else
                {
                    if (player.LeftHand == null)
                        player.LeftHand = item;
                    else
                    {
                        printLog("left hand occupied");
                        return;
                    }
                }
            }
            player.inventory.Remove(item);
        }
        public void Unequip()
        {
            
            if (player.InRightHand == true)
            {
                if (player.RightHand == null)
                {
                    printLog("right hand is empty");
                    return;
                }
                else
                {
                    player.inventory.Add(player.RightHand);
                    if (player.RightHand.IsTwoHanded == true)
                        player.LeftHand = null;
                    player.RightHand = null;
                }
            }
            else
            {
                if (player.LeftHand == null)
                {
                    printLog("left hand is empty");
                    return;
                }
                else
                {
                    player.inventory.Add(player.LeftHand);
                    if (player.LeftHand.IsTwoHanded == true)
                        player.RightHand = null;
                    player.LeftHand = null;
                }
            }
        }

        public void PrintHands()
        {
            for (int i = 0;i < 2; i++)
            {
                Console.SetCursorPosition(0, 20 + i);
                Console.Write("                                  ");
            }
            Console.SetCursorPosition(0, 20);
            
            if (player.InRightHand == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write("Left Hand: " + player.LeftHand);
            Console.ResetColor();
            Console.SetCursorPosition(0, 21);
            if(player.InRightHand == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write("Right Hand: " + player.RightHand);
            Console.ResetColor();



        }
        public void PrintSteering()
        {
            Console.SetCursorPosition(80, 0);
            Console.Write("##################################");
            Console.SetCursorPosition(80, 1);
            Console.Write("Steering:");
            Console.SetCursorPosition(80, 2);
            Console.Write("W,A,S,D - movement");
            Console.SetCursorPosition(80, 3);
            Console.Write("E - inventory");
            Console.SetCursorPosition(80, 4);
            Console.Write("F - equip");
            Console.SetCursorPosition(80, 5);
            Console.Write("Q - pickup item");
            Console.SetCursorPosition(80, 6);
            Console.Write("R - swap hands");



        }

        public bool TryWalk(int new_x, int new_y)
        {
            printLog("Trying to move to " + new_x + "," + new_y);
            if (world.map[new_x, new_y].IsWall)
            {
                printLog("wall unable to move");
                return false;
            }
            else
            {
                printTile(player.pos_x, player.pos_y);
                player.pos_x = new_x;
                player.pos_y = new_y;
                player.print_player();
                //printLog("Player move correctly");

            }
            return true;
        }


        public void Run()
        {
            Console.Clear();
            printWorld(world);
            printItems();
            player.print_player();
            PrintSteering();



            Console.SetCursorPosition(0, world.map.GetLength(1) + 2);
            while (true)
            {
                PrintStats();
                PrintInventory();
                PrintTileInfo();
                PrintHands();
                ConsoleKeyInfo key = Console.ReadKey(true);


                switch (key.Key)
                {
                    case ConsoleKey.W:
                        //move down
                        if (player.InInventory == true)
                        {
                            if (player.inventory_pos > 0)
                                player.inventory_pos--;
                            else
                                player.inventory_pos = player.inventory.Count - 1;
                        }
                        else
                            TryWalk(player.pos_x, player.pos_y - 1);
                        break;
                    case ConsoleKey.S:
                        //move up
                        if (player.InInventory == true)
                        {
                            if (player.inventory_pos < player.inventory.Count - 1)
                                player.inventory_pos++;
                            else
                                player.inventory_pos = 0;
                        }
                        else
                            TryWalk(player.pos_x, player.pos_y + 1);
                        break;
                    case ConsoleKey.D:
                        //move right
                        if (player.InInventory == false)
                            TryWalk(player.pos_x + 1, player.pos_y);
                        break;
                    case ConsoleKey.A:
                        if (player.InInventory == false)
                            TryWalk(player.pos_x - 1, player.pos_y);
                        //move left
                        break;
                    case ConsoleKey.E:
                        if (player.InInventory == false)
                        {
                            player.InInventory = true;
                            printLog("switch to inventory");
                        }
                        else
                        {
                            player.InInventory = false;
                            player.inventory_pos = 0;
                            printLog("switch out of inventory");
                        }
                        break;
                    case ConsoleKey.F:
                        if (player.InInventory == false)
                        {
                            printLog("unequping item");
                            Unequip();
                        }
                        else
                        {
                            printLog("equping item");
                            TryEuipItem();
                        }
                        break;
                    case ConsoleKey.Q:
                        if (player.InInventory == false)
                        {
                            if (TryPickUp())
                                printLog("item picked");
                            else
                                printLog("unable to pick up");
                        }
                        else
                        {
                            if (DropItem())
                                printLog("item dropped");
                            else
                                printLog("bombing failed");
                        }
                        break;
                    case ConsoleKey.R:
                        //changing hand
                        printLog("changing hands");
                        if (player.InRightHand)
                        {
                            player.InRightHand = false;
                        }
                        else
                            player.InRightHand = true;
                        break;
                    case ConsoleKey.Escape:

                        return;

                }
            }

        }
    }

}
