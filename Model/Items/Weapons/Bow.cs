using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Visitor_pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RPG_wiedzmin_wanna_be.Model.Items.Weapons
{
    internal class Bow : Weapon
    {
        public Bow(int pos_x = 1, int pos_y = 1, int _damage = 10) : base("Bow", _damage, pos_x, pos_y) { }

        public override int AcceptAttack(IAttackVisitor visitor, Player player)
        {
            return visitor.VisitLight(this, player);
        }

        public override int AcceptDeffence(IDefenseVisitor visitor, Player player)
        {
            return visitor.VisitLight(this, player);
        }
    }
}
