using RPG_wiedzmin_wanna_be.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Potions
{
    internal class HealthPotion : Potion
    {
        public HealthPotion(int pos_x = 0, int pos_y = 0):base("Health Potion",pos_x,pos_y) { }

        public override void ApplyEffects(IEntity entity)
        {
            entity.health += 20;
        }
    }
}
