using RPG_wiedzmin_wanna_be.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Potions
{
    internal class StrengthPotion:Potion
    {
        public StrengthPotion(int pos_x,int pos_y): base("Strength Potion",pos_x,pos_y) { }

        public override void ApplyEffects(IEntity entity)
        {
            entity.strength += 20;
        }
    }
}
