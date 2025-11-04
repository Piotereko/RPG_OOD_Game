using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Entity.Strategy;

namespace RPG_wiedzmin_wanna_be.Model.Entity
{
    internal class RandomEnemiesFactory
    {
        private Random random = new Random();

        public Enemy CreateRandomEnemy(int x, int y)
        {

            /*int enemy_behaviour = random.Next(0, 3);
            IEnemyBehavior behavior = enemy_behaviour switch
            {
                0 => new CalmBehaviour(),
                1=> new AgressiveBehaviour(),
                2=> new SkittishBehaviour()
            };*/

            IEnemyBehavior behavior = new AgressiveBehaviour();


            int enemy_type = random.Next(0, 3);
            return enemy_type switch
            {
                0 => new Zombie(x, y , behavior),
                1 => new Ogre(x, y,behavior),
                2 => new Goblin(x, y,behavior),

            };
        }
    }
}
