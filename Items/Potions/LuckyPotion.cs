using RPG_wiedzmin_wanna_be.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Potions
{
    internal class LuckyPotion: Potion
    {
        public LuckyPotion(int pos_x, int pos_y) : base("Lucky Potion", pos_x, pos_y) { }

        public override void ApplyEffects(IEntity entity)
        {
            entity.luck -= 10;
            if(entity.luck < 0)
                entity.luck = 0;
        }
    }
}
