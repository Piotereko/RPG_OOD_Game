using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Items;
using RPG_wiedzmin_wanna_be.Model.Items.Weapons;

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
