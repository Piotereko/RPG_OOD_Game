using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Weapons
{
    internal interface IWeapon : IItem
    {
        string Name { get; }

        int damage { get;}
        bool IsTwoHanded { get;}
    }
}
