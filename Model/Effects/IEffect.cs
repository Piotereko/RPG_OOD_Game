using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Effects
{
    public interface IEffect
    {
        string Name { get; set; }
        int Duration { get; set; }
        public Player target_entity { get; set; }
        public TurnManager turnManager { get; set; }
        void ApplyEffect(Player entity);
        void RemoveEffect(Player entity);
        void UpdateEffect();



    }
}
