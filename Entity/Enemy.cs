using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Entity
{
    internal abstract class Enemy : IEntity
    {
        public Enemy(string name,int pos_x, int pos_y, int strength, int health , int agression)
        {
            Name = name;
            this.pos_x = pos_x;
            this.pos_y = pos_y;
            this.strength = strength;
            this.health = health;
            this.agression = agression;
           
        }

        public int pos_x { get; set; }
        public int pos_y { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; } = 0;
        public int health { get; set; }
        public int luck { get; set; } = 0;
        public int agression { get; set; }
        public int wisdom { get; set; } = 0;

        string Name { get;}

        public abstract char EntitySing();

        public override string ToString()
        {
            return $"{Name} HP: {health} STR: {strength}";
        }
    }
}
