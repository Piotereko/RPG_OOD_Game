using RPG_wiedzmin_wanna_be.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Game
{
    internal class Render
    {
        public static void ClearArea(int x, int y, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(new string(' ', width));
            }
        }


        public static void printTile(Tile tile)
        {
            Console.SetCursorPosition(tile.pos_x, tile.pos_y);
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



        public static void printWorld(World _world)
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



        public static void printItems(World world)
        {
            foreach (IItem item in world.items)
            {
                Console.SetCursorPosition(item.X_position, item.Y_position);
                Console.Write("I");
            }
        }


        public static void PrintStats(Player player)
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



        public static void PrintInventory(Player player)
        {
            Console.SetCursorPosition(41, 11);
            Console.Write("################################");

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


        public static void PrintTileInfo(Tile tile)
        {
            Console.SetCursorPosition(80, 11);
            Console.Write("################################");
            Console.SetCursorPosition(80, 12);
            Console.WriteLine("Current tile:");

            ClearArea(80, 13, 30, 15);

            int index = 0;
            if (tile.items != null)
            {
                foreach (IItem item in tile.items)
                {
                    Console.SetCursorPosition(80, 13 + index++);
                    Console.Write(item.ToString());
                }
            }
        }



        public static void PrintSteering()
        {
            Console.SetCursorPosition(80, 0);
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
            Console.Write("R - swap hands");
        }



        public static void PrintHands(Player player)
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


        public static void PrintPlayer(Player player)
        {
            Console.SetCursorPosition(player.pos_x, player.pos_y);
            Console.WriteLine("B");
        }
    }
}
