using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Items;
using RPG_wiedzmin_wanna_be.Model.Items.Weapons;

namespace RPG_wiedzmin_wanna_be.Visitor_pattern
{
    internal class StealthAttackVisitor : IAttackVisitor
    {
        public int VisitHeavy(Weapon weapon, Player player) => (player.strength + player.agression) / 2;
        public int VisitLight(Weapon weapon, Player player) => 2 * (player.strength + player.agression);
        public int VisitMagic(Weapon weapon, Player player) => 1;
        public int VisitOther(IItem item, Player player) => 0;
    }
}
