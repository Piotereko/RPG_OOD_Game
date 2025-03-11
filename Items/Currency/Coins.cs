using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Currency
{
    internal class Coins : Currency
    {
        public Coins(int value, int pos_x, int pos_y) :base("Coins",value,pos_x,pos_y) { }

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
