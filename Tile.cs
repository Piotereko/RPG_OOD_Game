using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Items;

namespace RPG_wiedzmin_wanna_be
{
    internal class Tile
    {
        public int pos_x;
        public int pos_y;
        public List<IItem>? items;
        public bool IsWall { get; set; }
        public Tile() 
        {
            items = null;
            IsWall = false;
        }

    }
}
