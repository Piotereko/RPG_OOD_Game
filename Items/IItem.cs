using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items
{
    public interface IItem
    {
        string Name { get; }
        bool IsUsable { get; }

        bool IsEquipable {  get; }
        bool IsTwoHanded { get; set; }
        int X_position { get; set; }
        int Y_position {  get; set; }

        void PickMe(Player player);
        void DropMe(Player player);

        bool EquipMe(Player player);
        public void ApplyEffects(IEntity entity) { }
        public void RemoveEffects(IEntity entity) { }


        string ToString();
        
    }
}
