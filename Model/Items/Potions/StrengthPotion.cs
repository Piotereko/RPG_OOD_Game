using RPG_wiedzmin_wanna_be.Model.Effects;
using RPG_wiedzmin_wanna_be.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Model.Game;

namespace RPG_wiedzmin_wanna_be.Model.Items.Potions
{
    internal class StrengthPotion : Potion
    {
        public StrengthPotion(int pos_x, int pos_y) : base("Strength Potion", pos_x, pos_y, 5) { }

        public override void ApplyEffects(Player entity, TurnManager turn_manager)
        {
            var effect = new PowerEffect(entity, turn_manager);
            turn_manager.AddEffect(effect);
            effect.ApplyEffect(entity);
        }

        public override void RemoveEffects(Player entity)
        {

        }


    }
}
