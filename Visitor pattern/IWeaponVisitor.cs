using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items.Weapons;
using RPG_wiedzmin_wanna_be.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Visitor_pattern
{
    public interface IWeaponVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player);
        public int VisitLight(Weapon weapon, Player player);
        public int VisitMagic(Weapon weapon, Player player);
        public int VisitOther(IItem item, Player player);
    }

    public interface IAttackVisitor : IWeaponVisitor
    {
       
    }

    public interface IDefenseVisitor : IWeaponVisitor
    {
       
    }
}
