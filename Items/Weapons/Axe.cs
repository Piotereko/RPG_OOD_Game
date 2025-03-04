using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Weapons
{
    internal class Axe:IWeapon
    {
        public int damage { get; set; }
        public bool IsTwoHanded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsUsable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int X_position { get; set; }
        public int Y_position { get; set; }
    }
}
