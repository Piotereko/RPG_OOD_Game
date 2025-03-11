using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Items;
using RPG_wiedzmin_wanna_be.Items.Currency;
using RPG_wiedzmin_wanna_be.Items.Weapons;

namespace RPG_wiedzmin_wanna_be
{
    internal class World
    {
        public Tile[,] map; // two dimension array of instances of class tile 
        public List<IItem> items; // keeping track of items on grid

        public int height;
        public int width;


        public World(int _height = 20, int _width = 40) 
        {
            map = new Tile[_width, _height];

            height = _height;
            width = _width;

            items = new List<IItem>();

            for(int i = 0; i < height; i++ )
            {
                
                for(int j = 0; j < width; j++ )
                {
                    Tile tile = new Tile();
                    tile.pos_x = j; tile.pos_y = i;
                    map[j,i] = tile;
                } 
                
            }


            //boundaries walls
            for(int i = 0; i < width; i++ )
            {
                map[i,0].IsWall = true;
                map[i,height-1].IsWall = true;
            }

            for(int i = 0; i < height; i++ )
            {
                map[0,i].IsWall = true;
                map[width -1,i].IsWall = true;
            }

            //obstacles on the map
            for (int i = 4; i < 8; i++)
            {
                map[10,i].IsWall = true;
                map[30,i].IsWall = true;
            }
            for (int i = 12; i < 16; i++)
            {
                map[10, i].IsWall = true;
                map[30, i].IsWall = true;
            }
            for(int i = 10; i < 31; i++)
            {
                map[i,3].IsWall = true;
                map[i, 16].IsWall = true;
            }
            Axe axe = new Axe(10,4,4);
            Coins coin = new Coins(10, 5, 10);
            Weapon sword = new Sword(15, 5, 5);
            sword = new PowerFulEffect(sword);
            map[5,5].items.Add(sword);
            map[4,4].items = new List<IItem> { axe};
            map[5, 10].items.Add(coin);
            items.Add(axe);
            items.Add(coin);
            items.Add(sword);

            
        }
        public void printWorld()
        {
            for(int i = 0; i < height; i++ )
            {
                for(int j = 0; j < width; j++ )
                {
                    if (map[i,j].IsWall)
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

        public void printTile(int x, int y)
        {
            Tile tile = map[x, y];
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

    }
}
