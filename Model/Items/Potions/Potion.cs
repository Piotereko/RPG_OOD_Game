using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Items.Potions
{
    internal abstract class Potion : IItem
    {
        public string Name { get; }

        public bool IsUsable => true;

        public bool IsEquipable => false;

        public bool IsTwoHanded { get => false; set { } }

        public int X_position { get; set; }
        public int Y_position { get; set; }

        public int Duration { get; set; }



        public Potion(string name, int x_position, int y_position, int duration)
        {
            Name = name;
            X_position = x_position;
            Y_position = y_position;
            Duration = duration;
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
            return false;
        }

        public abstract void ApplyEffects(Player entity, TurnManager? turn_manager = null);

        public abstract void RemoveEffects(Player entity);

        public char ItemSign()
        {
            return 'P';
        }


    }
}

