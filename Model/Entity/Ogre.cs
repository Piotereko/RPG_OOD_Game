using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Entity
{

    internal class Ogre : Enemy
    {
        public Ogre(int pos_x, int pos_y) : base("Ogre", pos_x, pos_y, 25, 25, 25)
        {
        }

        public override char EntitySing()
        {
            return 'O';
        }
    }

}
