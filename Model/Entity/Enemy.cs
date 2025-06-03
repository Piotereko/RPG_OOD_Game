using RPG_wiedzmin_wanna_be.Model.Entity.Strategy;
using RPG_wiedzmin_wanna_be.Model.World;
using RPG_wiedzmin_wanna_be.View;
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
        public Enemy(string name, int pos_x, int pos_y, IEnemyBehavior behaviour ,int strength, int health, int agression)
        {
            Name = name;
            this.pos_x = pos_x;
            this.pos_y = pos_y;
           
            this.health = health;
            Behavior = behaviour;


        }

        public IEnemyBehavior Behavior { get; set; }

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

        public void Act(Player player, Dungeon dungeon)
        {
            Behavior?.Act(this, player, dungeon);
        }

        public void TryMove(int new_x,int new_y,Dungeon dungeon)
        {
            if (!dungeon.map[new_x][new_y].IsWall)
            {
                Render.Instance.printTile(dungeon.map[pos_x][pos_y]);
                pos_x = new_x;
                pos_y = new_y;
                Render.Instance.printEnemy(this);
            }
        }
    }
}
