using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Weapons
{
    public abstract class Decorator : Weapon
    {
        protected Weapon weapon;
        protected Decorator(Weapon _weapon) : base(_weapon.Name,_weapon.damage,_weapon.X_position,_weapon.Y_position)
        {
            weapon = _weapon;
        }

        public override string ToString()
        {
            return weapon.ToString();
        }
    }

    public class PowerFulEffect: Decorator
    {
        public PowerFulEffect(Weapon weapon) : base(weapon)
        {
            damage += 5;
        }

        public override string ToString() 
        {
            return $"{weapon} (PowerFul)";
        }
    }

    public class UnluckyEffect : Decorator
    {
        public UnluckyEffect(Weapon weapon, Player player) : base(weapon)
        {
            player.luck -= 5; 
        }

        public override string ToString()
        {
            return $"{weapon} (Unlucky)";
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
