using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Effects
{
    internal class PowerEffect : EffectBase
    {
        public PowerEffect(Player _entity, TurnManager manager) : base(_entity, "Power Effect", 5, manager) { }
        public override void ApplyEffect(Player entity)
        {
            entity.strength += 10;
        }

        public override void RemoveEffect(Player entity)
        {
            entity.strength -= 10;
            turnManager.RemoveEffect(this);
        }
    }
}
