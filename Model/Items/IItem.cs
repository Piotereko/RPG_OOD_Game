using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;

namespace RPG_wiedzmin_wanna_be.Model.Items
{
    public interface IItem
    {
        string Name { get; }
        bool IsUsable { get; }

        bool IsEquipable { get; }
        bool IsTwoHanded { get; set; }
        int X_position { get; set; }
        int Y_position { get; set; }

        void PickMe(Player player);
        void DropMe(Player player);

        bool EquipMe(Player player);
        public void ApplyEffects(Player entity, TurnManager? turn_manager = null);
        public void RemoveEffects(Player entity);

        char ItemSign();


        string ToString();

    }
}
