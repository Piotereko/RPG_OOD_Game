using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Game;
using RPG_wiedzmin_wanna_be.Visitor_pattern;

namespace RPG_wiedzmin_wanna_be.Items.Weapons
{
    public abstract class Weapon : IItem
    {
        public string Name {  get; set; }

        public int damage {  get; set; }

        public bool IsUsable => true;

        public bool IsEquipable => true;


        public int X_position { get; set; }
        public int Y_position { get; set; }
        public bool IsTwoHanded {  get; set; } = false;

        public virtual int Damage => damage;

        public Weapon(string name, int _damage, int pos_x, int pos_y)
        {
            Name = name;
            damage = _damage;
            X_position = pos_x;
            Y_position = pos_y;
        }

        public abstract int AcceptAttack(IAttackVisitor visitor, Player player);
        public abstract int AcceptDeffence(IDefenseVisitor visitor, Player player);

        public override string ToString()
        {
            return $"{Name} ({Damage})";
        }

        void IItem.PickMe(Player player)
        {
            return;
        }

        void IItem.DropMe(Player player)
        {
            return;
        }

        public bool EquipMe(Player player)
        {
            return true;
        }
        //public virtual void ApplyEffects(IEntity entity) { }
        public virtual void RemoveEffects(Player entity) { }

        public virtual char ItemSign()
        {
            return 'W';
        }

       public virtual void ApplyEffects(Player entity, TurnManager? turn_manager = null)
       {
           
       }
    }
}
