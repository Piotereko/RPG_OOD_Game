using RPG_wiedzmin_wanna_be.Model.Entity.Strategy;

namespace RPG_wiedzmin_wanna_be.Model.Entity
{
    internal class Zombie : Enemy
    {
        public Zombie(int pos_x, int pos_y,IEnemyBehavior behavior) : base("Zombie", pos_x, pos_y,behavior, 10, 10, 10)
        {
        }

        public override char EntitySing()
        {
            return 'Z';
        }
    }
}
