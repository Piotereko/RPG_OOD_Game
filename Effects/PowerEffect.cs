using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Effects
{
    internal class PowerEffect : EffectBase
    {
        public PowerEffect(IEntity _entity, TurnManager manager) :base(_entity,"Power Effect",5, manager) { }
        public override void ApplyEffect(IEntity entity)
        {
           entity.strength += 10;
        }

        public override void RemoveEffect(IEntity entity)
        {
            entity.strength -= 10;
            turnManager.RemoveEffect(this);
        }
    }
}
