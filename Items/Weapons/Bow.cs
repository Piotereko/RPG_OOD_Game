using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RPG_wiedzmin_wanna_be.Items.Weapons
{
    internal class Bow : Weapon
    {
        public Bow(int _damage = 10, int pos_x  = 0, int pos_y = 0) : base("Bow",_damage, pos_x, pos_y) { }
    }
}
