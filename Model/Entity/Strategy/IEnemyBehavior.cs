using RPG_wiedzmin_wanna_be.Model.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Entity.Strategy
{
    public interface IEnemyBehavior
    {
        void Act(Enemy enemy, Player player, Dungeon dungeon);

        public bool Move(Enemy enemy, Player player, Dungeon dungeon);
        public bool Attack(Enemy enemy, Player player, Dungeon dungeon);
    }
}
