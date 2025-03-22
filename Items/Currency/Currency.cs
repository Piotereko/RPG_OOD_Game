using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Entity;

namespace RPG_wiedzmin_wanna_be.Items.Currency
{
    public abstract class Currency : IItem
    {
        public string Name { get; }

        public bool IsUsable => false;
        public bool IsEquipable => false;
        public bool IsTwoHanded { get => false ; set { } }

        public int X_position { get; set; }
        public int Y_position { get; set; }

        public int Value {get;}
        
        protected Currency(string name, int value, int pos_x, int pos_y) 
        {
            Name = name;
            Value = value;
            X_position = pos_x;
            Y_position = pos_y;
        }

        public override string ToString()
        {
            return $"{Name} x{Value}";
        }

        public virtual void PickMe(Player player) { }
       

        public virtual void DropMe(Player player) { }

        public bool EquipMe(Player player)
        {
            return true;
        }

        public void ApplyEffects(IEntity entity)
        {
            return;
        }

        public void RemoveEffects(IEntity entity)
        {
            return;
        }

        public char ItemSign()
        {
            return '$';
        }
    }
}
