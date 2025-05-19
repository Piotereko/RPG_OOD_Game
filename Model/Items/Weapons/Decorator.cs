using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Visitor_pattern;

namespace RPG_wiedzmin_wanna_be.Model.Items.Weapons
{
    public abstract class Decorator : Weapon
    {
        protected Weapon weapon;
        protected Decorator(Weapon _weapon) : base(_weapon.Name, _weapon.damage, _weapon.X_position, _weapon.Y_position)
        {
            weapon = _weapon;
        }

        public override int AcceptAttack(IAttackVisitor visitor, Player player)
        {
            return weapon.AcceptAttack(visitor, player);
        }

        public override int AcceptDeffence(IDefenseVisitor visitor, Player player)
        {
            return weapon.AcceptDeffence(visitor, player);
        }
        public override char ItemSign()
        {
            return 'O';
        }
        public override int Damage => weapon.damage;
        public override string ToString()
        {
            return weapon.ToString();
        }
    }

    public class PowerFulEffect : Decorator
    {
        public PowerFulEffect(Weapon weapon) : base(weapon)
        {
        }
        public override int Damage => weapon.Damage + 100;

        public override string ToString()
        {
            return $"{weapon} (PowerFul)";
        }
    }

    public class UnluckyEffect : Decorator
    {
        public UnluckyEffect(Weapon weapon) : base(weapon)
        {

        }
        public override void ApplyEffects(Player entity, TurnManager? turn_manager = null)
        {
            entity.luck -= 5;
        }
        public override void RemoveEffects(Player entity)
        {
            entity.luck += 5;
        }
        public override string ToString()
        {
            return $"{weapon} (Unlucky) ";
        }
    }

    public class TwoHandnes : Decorator
    {
        public TwoHandnes(Weapon weapon) : base(weapon)
        {
            IsTwoHanded = true;
        }
        public override string ToString()
        {
            return $"{weapon} (Two Handed)";
        }
    }
}
