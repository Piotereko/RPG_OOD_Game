using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Items.Randoms
{
    internal class LuckyBox : Random
    {
        public LuckyBox(int pos_x, int pos_y) : base("LuckyBox", pos_x, pos_y) { }
    }
}
