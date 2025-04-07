using RPG_wiedzmin_wanna_be.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Potions
{
    internal abstract class Potion : IItem
    {
        public string Name { get; }

        public bool IsUsable => true;

        public bool IsEquipable => false;

        public bool IsTwoHanded { get => false; set { } }

        public int X_position { get; set; }
        public int Y_position { get; set; }

        public Potion(string name, int x_position, int y_position)
        {
            Name = name;
            X_position = x_position;
            Y_position = y_position;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public void DropMe(Player player)
        {
            return;
        }

        public void PickMe(Player player)
        {
            return;
        }
        public bool EquipMe(Player player)
        {
            player.inventory.Remove(this);
            return true;
        }

        public abstract void ApplyEffects(IEntity entity);

        public void RemoveEffects(IEntity entity)
        {
            return;
        }

        public char ItemSign()
        {
           return 'P';
        }
    }
}

