using RPG_wiedzmin_wanna_be.Entity;
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


        public EffectBase(string name, int duration) 
        {
            Name = name;
            Duration = duration;
        }

        public abstract void ApplyEffect(Player player);

        public abstract void RemoveEffect(Player player);

        public void UpdateEffect()
        {
            if (Duration > 0)
                Duration--;
        }


    }
}
