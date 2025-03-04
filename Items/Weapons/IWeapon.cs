using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Weapons
{
    internal interface IWeapon : IItem
    {

        public int damage { get; set; }
        public bool IsTwoHanded { get; set; }
    }
}
