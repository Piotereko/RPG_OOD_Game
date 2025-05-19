using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Items.Randoms
{
    internal class Bottle : Random
    {
        public Bottle(int pos_x = 0, int pos_y = 0) : base("Bottle", pos_x, pos_y) { }
    }
}
