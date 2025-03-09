using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Randoms
{
    internal class LuckyBox : IItem
    {
        public bool IsUsable => false;

        public int X_position {  get; set; }
        public int Y_position {get; set; }

        public string Name => "LuckyBox";
    }
}
