using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Model.Items.Currency;
using RPG_wiedzmin_wanna_be.Model.Items.Weapons;
using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Items;


namespace RPG_wiedzmin_wanna_be.Model.World
{
    public class Dungeon
    {
        public Tile[][] map { get; set; } // two dimension array of instances of class tile 
        public List<IItem> items; // keeping track of items on grid
        public List<Enemy> enemies;

        public int height;
        public int width;


        public bool HasWeapons;
        public bool HasPotions;

        public List<(int x, int y)> chambers;

        public Dungeon(int _height = 20, int _width = 40)
        {
            height = _height;
            width = _width;
            map = new Tile[width][];

           

            items = new List<IItem>();
            enemies = new List<Enemy>();
            chambers = new List<(int, int)>();

            for (int i = 0; i < width; i++)
            {
                map[i] = new Tile[height];
                for (int j = 0; j < height; j++)
                {
                    Tile tile = new Tile();
                    tile.pos_x = i; tile.pos_y = j;
                    map[i][j] = tile;
                }

            }


            //boundaries walls
            for (int i = 0; i < width; i++)
            {
                map[i][0].IsWall = true;
                map[i][height - 1].IsWall = true;
            }

            for (int i = 0; i < height; i++)
            {
                map[0][ i].IsWall = true;
                map[width - 1][i].IsWall = true;
            }


        }
        public void AddItem(IItem item)
        {
            map[item.X_position][item.Y_position].items.Add(item);
            items.Add(item);
        }
    }
}
