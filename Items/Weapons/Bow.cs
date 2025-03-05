using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Weapons
{
    internal class Bow : IWeapon
    {
        public int damage { get; set; }

        public bool IsTwoHanded => true;
        public bool IsUsable => true;
        public int X_position { get; set; }
        public int Y_position { get; set; }

        public string Name => "Bow";
    }
}
