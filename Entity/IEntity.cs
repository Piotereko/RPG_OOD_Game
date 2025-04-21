using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Entity
{
    public interface IEntity
    {
        public int pos_x { get; set; }
        public int pos_y { get; set; }
        
        public int health { get; set; }
        

        char EntitySing();
    }
}
