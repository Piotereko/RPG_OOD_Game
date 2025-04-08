using RPG_wiedzmin_wanna_be.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Effects
{
    internal class LuckEffect : EffectBase
    {
        int original_luck;
        //int inital_duration;
        public LuckEffect(IEntity _entity, int duration) : base(_entity, "Luck Effect", duration)
        {
            original_luck = _entity.luck;
            //inital_duration = duration;
        }

        public override void ApplyEffect(IEntity entity)
        {
            entity.luck = original_luck * (Duration + 1);
        }

        public override void RemoveEffect(IEntity entity)
        {
            entity.luck = original_luck;
        }

        public override void UpdateEffect()
        {
            base.UpdateEffect();
            ApplyEffect(target_entity);
        }
    }
}
