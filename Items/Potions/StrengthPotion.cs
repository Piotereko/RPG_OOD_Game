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
        public StrengthPotion(int pos_x,int pos_y): base("Strength Potion",pos_x,pos_y, 5) { }

        public override void ApplyEffects(IEntity entity)
        {
            entity.strength += 20;
        }

        public override void RemoveEffects(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
