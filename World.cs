using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Items;

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
            map = new Tile[_height, _width];

            height = _height;
            width = _width;

            items = new List<IItem>();

            for(int i = 0; i < height; i++ )
            {
                
                for(int j = 0; j < width; j++ )
                {
                    Tile tile = new Tile();
                    tile.pos_x = j; tile.pos_y = i;
                    map[i,j] = tile;
                } 
                
            }


            //boundaries walls
            for(int i = 0; i < height; i++ )
            {
                map[i,0].IsWall = true;
                map[i,width-1].IsWall = true;
            }

            for(int i = 0; i < width; i++ )
            {
                map[0,i].IsWall = true;
                map[height -1,i].IsWall = true;
            }


            //obstacles on the map
            for (int i = 4; i < 8; i++)
            {
                map[i, 10].IsWall = true;
                map[i, 30].IsWall = true;
            }
            for (int i = 12; i < 16; i++)
            {
                map[i, 10].IsWall = true;
                map[i, 30].IsWall = true;
            }
            for(int i = 10; i < 31; i++)
            {
                map[3,i].IsWall = true;
                map[16,i].IsWall = true;
            }
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

    }
}
