using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Visitor_pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Weapons
{
    internal class Axe: Weapon
    {
        public Axe(int pos_x  = 1, int pos_y = 1, int _damage = 15) : base("Axe",_damage, pos_x, pos_y) { }

        public override int AcceptAttack(IAttackVisitor visitor, Player player)
        {
           return visitor.VisitMagic(this,player);
        }

        public override int AcceptDeffence(IDefenseVisitor visitor, Player player)
        {
            return visitor.VisitMagic(this, player);
        }
    }
}
