using RPG_wiedzmin_wanna_be.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Game
{
    public class TurnManager
    {
        public List<IEffect> effects = new List<IEffect>();

        public void AddEffect(IEffect effect)
        {
            effects.Add(effect);
        }
        public void RemoveEffect(IEffect effect)
        {
            effects.Remove(effect);
        }

        public void UpdateEffects()
        {

            for (int i = 0; i < effects.Count; i++)
            {
                IEffect effect = effects[i];

                effect.UpdateEffect();

                /*if (effect.Duration == 0)
                {
                    effect.RemoveEffect(effect.target_entity);
                    effects.RemoveAt(i);
                }*/
            }
            /*foreach (IEffect effect in effects)
            {
                effect.UpdateEffect();
            }*/
        }
    }
}
