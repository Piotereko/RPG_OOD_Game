namespace RPG_wiedzmin_wanna_be.Entity
{
    internal class Zombie : Enemy
    {
        public Zombie(int pos_x, int pos_y) : base("Zombie", pos_x, pos_y, 10, 10, 10)
        {
        }

        public override char EntitySing()
        {
            return 'Z';
        }
    }
}
