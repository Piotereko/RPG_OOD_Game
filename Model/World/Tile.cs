using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Model.Items;

namespace RPG_wiedzmin_wanna_be.Model.World
{
    public class Tile
    {
        public int pos_x {  get; set; }
        public int pos_y { get; set; }
        public List<IItem> items { get; set; }
        public bool IsWall { get; set; }
        public Tile()
        {
            //items = null;
            IsWall = false;
            items = new List<IItem>();
        }

    }
}
