using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items.Weapons;
using RPG_wiedzmin_wanna_be.Items;

namespace RPG_wiedzmin_wanna_be.Visitor_pattern
{
    internal class MagicDefenseVisitor : IDefenseVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => player.luck;
        public int VisitLight(Weapon weapon, Player player) => player.luck;
        public int VisitMagic(Weapon weapon, Player player) => player.wisdom * 2;
        public int VisitOther(IItem item, Player player) => player.luck;
    }
}
