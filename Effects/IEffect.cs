using RPG_wiedzmin_wanna_be.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Effects
{
    public interface IEffect
    {
        string Name { get; set; }
        int Duration { get; set; }
        public IEntity target_entity { get; set; }
        void ApplyEffect(IEntity entity);
        void RemoveEffect(IEntity entity);
        void UpdateEffect();

    

    }
}
