using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Entity;

namespace RPG_wiedzmin_wanna_be.Items.Randoms
{
    internal class Random : IItem
    {
        public string Name { get; }

        public bool IsUsable => false;

        public bool IsEquipable => false;

        public bool IsTwoHanded { get => false; set {  } }

        public int X_position {get; set;}
        public int Y_position {get;set;}

        public Random(string name, int x_position, int y_position)
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
            return true;
        }

        public void ApplyEffects(IEntity entity)
        {
            return;
        }
    }
}
