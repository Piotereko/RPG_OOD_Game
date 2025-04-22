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

    internal class NormalAttackVisitor : IAttackVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => player.strength + player.agression;
        public int VisitLight(Weapon weapon, Player player) => player.dexterity + player.luck;
        public int VisitMagic(Weapon weapon, Player player) => player.wisdom;
        public int VisitOther(IItem item, Player player) => 0;
    }

    internal class StealthAttackVisitor : IAttackVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => (player.strength + player.agression) / 2;
        public int VisitLight(Weapon weapon, Player player) => 2 * (player.strength + player.agression);
        public int VisitMagic(Weapon weapon, Player player) => 1;
        public int VisitOther(IItem item, Player player) => 0;
    }

    internal class MagicAttackVisitor : IAttackVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => 1;
        public int VisitLight(Weapon weapon, Player player) => 1;
        public int VisitMagic(Weapon weapon, Player player) => player.wisdom;
        public int VisitOther(IItem item, Player player) => 0;
    }

    internal class NormalDefenseVisitor : IDefenseVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => player.strength + player.luck;
        public int VisitLight(Weapon weapon, Player player) => player.dexterity + player.luck;
        public int VisitMagic(Weapon weapon, Player player) => player.dexterity + player.luck;
        public int VisitOther(IItem item, Player player) => player.dexterity;
    }

    internal class StealthDefenseVisitor : IDefenseVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => player.strength;
        public int VisitLight(Weapon weapon, Player player) => player.dexterity;
        public int VisitMagic(Weapon weapon, Player player) => 0;
        public int VisitOther(IItem item, Player player) => 0;
    }

    internal class MagicDefenseVisitor : IDefenseVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => player.luck;
        public int VisitLight(Weapon weapon, Player player) => player.luck;
        public int VisitMagic(Weapon weapon, Player player) => player.wisdom * 2;
        public int VisitOther(IItem item, Player player) => player.luck;
    }
}
