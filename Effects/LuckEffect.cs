using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Game;
using System.Linq;

namespace RPG_wiedzmin_wanna_be.Effects
{
    internal class LuckEffect : EffectBase
    {
        private static int base_luck;
        private static bool base_captured = false;
        private int multiplier;

        public LuckEffect(Player entity, int duration, TurnManager manager) : base(entity, "Luck Effect", duration, manager)
        {
            if (!base_captured)
            {
                base_luck = entity.luck;
                base_captured = true;
            }
            multiplier = duration + 1;
            RecalculateLuck();
        }

        public override void ApplyEffect(Player entity)
        {
            RecalculateLuck();
        }

        public override void RemoveEffect(Player entity)
        {
            bool last_effect = true;

            foreach (var effect in turnManager.effects)
            {
                if (effect.GetType() == typeof(LuckEffect) && effect != this)
                {
                    last_effect = false;
                }
            }
            if (last_effect)
            {
                entity.luck = base_luck;
                base_captured = false;
            }
            else
            {
                RecalculateLuck();
            }
            turnManager.RemoveEffect(this);
        }

        public override void UpdateEffect()
        {
            base.UpdateEffect();
            if (!IsExpired)
            {
                multiplier = Duration + 1;
                RecalculateLuck();
            }
        }

        private void RecalculateLuck()
        {
            int totalMultiplier = 1;

            for (int i = 0; i < turnManager.effects.Count; i++)
            {
                IEffect effect = turnManager.effects[i];
                if (effect.GetType() == typeof(LuckEffect))
                {
                    LuckEffect luckEffect = (LuckEffect)effect;
                    totalMultiplier *= luckEffect.multiplier;
                }
            }

            target_entity.luck = base_luck * totalMultiplier;
        }
    }
}