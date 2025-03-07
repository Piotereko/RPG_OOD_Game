using RPG_wiedzmin_wanna_be.Items;
using RPG_wiedzmin_wanna_be.Items.Weapons;
using System.Diagnostics;

namespace RPG_wiedzmin_wanna_be
{
    internal class GameEngine
    {
        World world = new World();
        Player player = new Player();


        public void printTile(int x, int y)
        {
            Tile tile = world.map[x,y];
            Console.SetCursorPosition(x, y);
            if (tile.IsWall)
            {
                Console.Write("█");
            }
            else if (tile.items != null)
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
            foreach(IItem item in world.items)
            {
                Console.SetCursorPosition(item.X_position, item.Y_position);
                Console.Write("I");
            }
        }
        public void printLog(string  msg)
        {
            Console.SetCursorPosition(0, 25);
            Console.WriteLine(msg);
        }

        public void PrintStats()
        {
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
            //Console.SetCursorPosition(0, 0);
        }

        public void PrintInventory()
        {
            Console.SetCursorPosition(41, 9);
            Console.Write("################################");
            Console.SetCursorPosition(41, 10);
            Console.WriteLine("Player Inventory:");

        }

        public void PrintTileInfo()
        {
            Console.SetCursorPosition(80, 9);
            Console.Write("################################");
            Console.SetCursorPosition(80, 10);
            Console.WriteLine("Current tile:");

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

        public bool TryWalk(int  new_x, int new_y)
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
                ConsoleKeyInfo key = Console.ReadKey(true);


                switch (key.Key)
                {
                    case ConsoleKey.W:
                        TryWalk(player.pos_x, player.pos_y - 1);
                        break;
                    case ConsoleKey.S:
                        TryWalk(player.pos_x,player.pos_y+1);
                        break;
                    case ConsoleKey.D:
                        //move right
                        TryWalk(player.pos_x + 1, player.pos_y);
                        break;  
                    case ConsoleKey.A:
                        TryWalk(player.pos_x - 1, player.pos_y);
                        //move left
                        break;
                    case ConsoleKey.E:
                        //inventory
                        break;
                    case ConsoleKey.F:
                        //equip
                        break;
                    case ConsoleKey.Escape:

                        return;

                }
            }

        }
    }

}
