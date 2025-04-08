using RPG_wiedzmin_wanna_be.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Effects
{
    internal class HealthEffect : EffectBase
    {
        
        public HealthEffect(IEntity _entity) : base(_entity, "Health Effect", -1)
        {
           
        }

        public override void ApplyEffect(IEntity entity)
        {
            entity.health += 10;
        }

        public override void RemoveEffect(IEntity entity)
        {
            entity.health -= 10;
            if (entity.health < 0)
                entity.health = 0;
        }
    }
}
