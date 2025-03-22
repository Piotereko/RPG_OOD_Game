using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.Entity;

namespace RPG_wiedzmin_wanna_be.Items.Currency
{
    internal class Coins : Currency
    {
        public Coins(int pos_x = 1, int pos_y = 1, int value = 10 ) :base("Coins",value,pos_x,pos_y) { }

        public override void PickMe(Player player)
        {
            player.coins_amount += Value;
        }

        public override void DropMe(Player player)
        {
            player.coins_amount -= Value;
        }


    }
}
