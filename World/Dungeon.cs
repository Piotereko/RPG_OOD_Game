using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items;
using RPG_wiedzmin_wanna_be.Items.Currency;
using RPG_wiedzmin_wanna_be.Items.Weapons;


namespace RPG_wiedzmin_wanna_be.World
{
    internal class Dungeon
    {
        public Tile[,] map; // two dimension array of instances of class tile 
        public List<IItem> items; // keeping track of items on grid
        public List<Enemy> enemies;

        public int height;
        public int width;


        public bool HasWeapons;
        public bool HasPotions;


        public Dungeon(int _height = 20, int _width = 40)
        {
            map = new Tile[_width, _height];

            height = _height;
            width = _width;

            items = new List<IItem>();
            enemies = new List<Enemy>();

            for (int i = 0; i < height; i++)
            {

                for (int j = 0; j < width; j++)
                {
                    Tile tile = new Tile();
                    tile.pos_x = j; tile.pos_y = i;
                    map[j, i] = tile;
                }

            }


            //boundaries walls
            for (int i = 0; i < width; i++)
            {
                map[i, 0].IsWall = true;
                map[i, height - 1].IsWall = true;
            }

            for (int i = 0; i < height; i++)
            {
                map[0, i].IsWall = true;
                map[width - 1, i].IsWall = true;
            }

            //obstacles on the map
            /*for (int i = 4; i < 8; i++)
            {
                map[10, i].IsWall = true;
                map[30, i].IsWall = true;
            }
            for (int i = 12; i < 16; i++)
            {
                map[10, i].IsWall = true;
                map[30, i].IsWall = true;
            }
            for (int i = 10; i < 31; i++)
            {
                map[i, 3].IsWall = true;
                map[i, 16].IsWall = true;
            }*/

          /*  AddItem(new Axe(4, 4));
            AddItem(new Bow(5, 5));
            AddItem(new Gold(4, 4));
            AddItem(new Coins(25, 12, 100));
            Weapon powerfulunluckysword = new UnluckyEffect(new PowerFulEffect(new Sword(5, 6)));
            IItem unluckyaxe = new UnluckyEffect(new Axe(4, 4));
            IItem twohandedbow = new TwoHandnes(new Bow(5, 5));
            IItem powerfulbow = new PowerFulEffect(new Bow(6, 6));
            AddItem(powerfulunluckysword);
            AddItem(twohandedbow);
            AddItem(unluckyaxe);
            AddItem(powerfulbow);*/


        }
        public void AddItem(IItem item)
        {
            map[item.X_position, item.Y_position].items.Add(item);
            items.Add(item);
        }
    }
}
