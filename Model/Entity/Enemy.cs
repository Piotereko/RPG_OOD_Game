using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Entity
{
    public abstract class Enemy : IEntity
    {
        public Enemy(string name, int pos_x, int pos_y, int strength, int health, int agression)
        {
            Name = name;
            this.pos_x = pos_x;
            this.pos_y = pos_y;

            this.health = health;


        }

        public int pos_x { get; set; }
        public int pos_y { get; set; }

        public int health { get; set; }

        public int attack_val { get; protected set; }
        public int armour { get; protected set; }

        string Name { get; }

        public abstract char EntitySing();

        public override string ToString()
        {
            return $"{Name} HP: {health}";
        }

        public bool IsAlive => health > 0;

        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(0, damage - armour);
            health -= actualDamage;
        }
    }
}
