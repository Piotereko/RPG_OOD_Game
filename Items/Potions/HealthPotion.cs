using RPG_wiedzmin_wanna_be.Effects;
using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Potions
{
    internal class HealthPotion : Potion
    {
        public HealthPotion( int pos_x = 0, int pos_y = 0):base("Health Potion",pos_x,pos_y,5) { }

        public override void ApplyEffects(IEntity entity,TurnManager turn_manager)
        {
            var effect = new HealthEffect(entity,turn_manager);
            turn_manager.AddEffect(effect);
            effect.ApplyEffect(entity);
        }

        public override void RemoveEffects(IEntity entity)
        {
            
        }

        
    }
}
