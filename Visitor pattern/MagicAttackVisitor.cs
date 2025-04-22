using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items.Weapons;
using RPG_wiedzmin_wanna_be.Items;


namespace RPG_wiedzmin_wanna_be.Visitor_pattern
{
    internal class MagicAttackVisitor : IAttackVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => 1;
        public int VisitLight(Weapon weapon, Player player) => 1;
        public int VisitMagic(Weapon weapon, Player player) => player.wisdom;
        public int VisitOther(IItem item, Player player) => 0;
    }
}
