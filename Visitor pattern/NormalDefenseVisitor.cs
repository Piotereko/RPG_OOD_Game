using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items.Weapons;
using RPG_wiedzmin_wanna_be.Items;

namespace RPG_wiedzmin_wanna_be.Visitor_pattern
{
    internal class NormalDefenseVisitor : IDefenseVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => player.strength + player.luck;
        public int VisitLight(Weapon weapon, Player player) => player.dexterity + player.luck;
        public int VisitMagic(Weapon weapon, Player player) => player.dexterity + player.luck;
        public int VisitOther(IItem item, Player player) => player.dexterity;
    }
}
