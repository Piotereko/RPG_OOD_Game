using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Model.Entity;

namespace RPG_wiedzmin_wanna_be.Model.Entity
{
    internal class RandomEnemiesFactory
    {
        private Random random = new Random();

        public Enemy CreateRandomEnemy(int x, int y)
        {
            int enemy_type = random.Next(0, 3);
            return enemy_type switch
            {
                0 => new Zombie(x, y),
                1 => new Ogre(x, y),
                2 => new Goblin(x, y),

            };
        }
    }
}
