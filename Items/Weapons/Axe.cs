using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Weapons
{
    internal class Axe: Weapon
    {
        public Axe(int _damage = 15, int pos_x  = 0, int pos_y = 0) : base("Axe",_damage, pos_x, pos_y) { }

    }
}
