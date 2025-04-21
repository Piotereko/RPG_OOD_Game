using RPG_wiedzmin_wanna_be.Effects;
using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Potions
{
    internal class AntidothumPotion : Potion
    {
        public AntidothumPotion(int x_position, int y_position) : base("Antidothum", x_position, y_position,0)
        {
        }

        public override void ApplyEffects(Player entity, TurnManager? turn_manager = null)
        {
            if (turn_manager == null) return;
            var activeEffects = new List<IEffect>(turn_manager.effects);

            foreach (var effect in activeEffects)
            {
                effect.RemoveEffect(effect.target_entity);
                turn_manager.RemoveEffect(effect);
            }

            Logger.PrintLog("All active effects have been removed!");
        }

        public override void RemoveEffects(Player entity)
        {
           
        }
    }
}
