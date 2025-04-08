using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Effects
{
    public abstract class EffectBase : IEffect
    {
        public string Name { get; set; }

        public int Duration { get; set; }

        public bool IsExpired => Duration <= 0 && Duration != -1;


        
        public IEntity target_entity { get ; set ; }
        public TurnManager turnManager { get; set ; }

        public EffectBase(IEntity _entity, string name, int duration,TurnManager manager) 
        {
            Name = name;
            Duration = duration;
            target_entity = _entity;
            turnManager = manager;

        }

        public abstract void ApplyEffect(IEntity entity);

        public abstract void RemoveEffect(IEntity entity);

        public virtual void UpdateEffect()
        {
            if (Duration > 0)
                Duration--;
            if(Duration == 0)
            {
                RemoveEffect(target_entity);
            }
        }

    }
}
