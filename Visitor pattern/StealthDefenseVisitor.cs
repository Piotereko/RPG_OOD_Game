using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items.Weapons;
using RPG_wiedzmin_wanna_be.Items;

namespace RPG_wiedzmin_wanna_be.Visitor_pattern
{
    internal class StealthDefenseVisitor : IDefenseVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => player.strength;
        public int VisitLight(Weapon weapon, Player player) => player.dexterity;
        public int VisitMagic(Weapon weapon, Player player) => 0;
        public int VisitOther(IItem item, Player player) => 0;
    }
}
