using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Entity
{

    internal class Goblin : Enemy
    {
        public Goblin(int pos_x, int pos_y) : base("Goblin", pos_x, pos_y, 15, 15, 15)
        {
        }

        public override char EntitySing()
        {
            return 'G';
        }
    }

}
