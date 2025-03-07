using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be
{
    internal class GameEngine
    {
        World world = new World();
        Player player = new Player();
       
        public void printWorld(World _world)
        {
            for (int i = 0; i < _world.height; i++)
            {
                for (int j = 0; j < _world.width; j++)
                {
                    if (_world.map[i, j].IsWall)
                    {
                        Console.Write("█");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void PrintStats()
        {
            Console.SetCursorPosition(41, 0);
            Console.Write("############################################");
            Console.SetCursorPosition(41, 1);
            Console.WriteLine("Player Statistic:");
            Console.SetCursorPosition(0, 0);  
        }

        public void PrintInventory()
        {

        }

        public void PrintTileInfo()
        {

        }

        public void PrintSteering()
        {

        }


        public void Run()
        {
            Console.Clear();
            printWorld(world);

            player.print_player();

            PrintStats();
            //PrintStats();

            Console.SetCursorPosition(0, world.map.GetLength(0) + 2);

        }
    }
    
}
