using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Effects
{
    internal class HealthEffect : EffectBase
    {
        
        public HealthEffect(Player _entity,TurnManager manager) : base(_entity, "Health Effect", -1,manager)
        {
           
        }

        public override void ApplyEffect(Player entity)
        {
            entity.health += 10;
        }

        public override void RemoveEffect(Player entity)
        {
            entity.health -= 10;
            if (entity.health < 0)
                entity.health = 0;
            turnManager.RemoveEffect(this);
        }
    }
}
