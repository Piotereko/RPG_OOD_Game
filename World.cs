using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Items;

namespace RPG_wiedzmin_wanna_be
{
    internal class World
    {
        private Tile[,] map; // two dimension array of instances of class tile 
        public List<IItem> items; // keeping track of items on grid


        public World() 
        {
            for(int i = 0; i < 20; i++ )
            {
                
                for(int j = 0; j < 40; j++ )
                {
                    Tile tile = new Tile();
                    map[i,j] = tile;
                }
                
            }
            for(int i = 0; i < 20; i++ )
            {
                map[i,0].IsWall = true;
                map[i,39].IsWall = true;
            }
            for(int i = 0; i < 40; i++ )
            {
                map[0,i].IsWall = true;
                map[19,i].IsWall = true;
            }
            for(int i = 4; i < 8; i++)
            {
                map[i,10].IsWall = true;
                map[i,15].IsWall = true;
            }
            for(int i = 12; i < 16; i++)
            {
                map[i, 10].IsWall = true;
                map[i,15].IsWall = true;
            }
        }
    }
}
