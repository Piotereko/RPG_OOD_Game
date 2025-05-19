using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Model.Items;
using RPG_wiedzmin_wanna_be.Model.Items.Weapons;
using RPG_wiedzmin_wanna_be.Model.Entity;

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
