using RPG_wiedzmin_wanna_be.Items.Currency;
using RPG_wiedzmin_wanna_be.Items.Potions;
using RPG_wiedzmin_wanna_be.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Factory
{
    public class RandomItemsFactory
    {
        private Random random = new Random();

        public IItem CreateRadnomWeapon(int x, int y)
        {
            int weapon_type = random.Next(0, 3);
            return weapon_type switch
            {
                0 => new Sword(x, y),
                1 => new Axe(x, y),
                2 => new Bow(x, y),
                _ => new Sword(x,y),
            };
        }

        public IItem CreateRandomModifiedWeapon(int x, int y) 
        {
            int weapon_type = random.Next(0, 3);
            Weapon weapon = weapon_type switch
            {
                0 => new Sword(x, y),
                1 => new Axe(x, y),
                2 => new Bow(x, y),
                _ => new Sword(x, y),
            };
            
            int modification = random.Next(0,3);
            switch(modification)
            {
                case 0:
                    return new PowerFulEffect(weapon);
                case 1:
                    return new UnluckyEffect(weapon);
                case 2:
                    return new TwoHandnes(weapon);
                default:
                    return weapon;
            }
        }

        public IItem CreateRandomPotion(int x, int y)
        {
            int potion_type = random.Next(0, 3);
            return potion_type switch
            {
                0 => new HealthPotion(x, y),
                1 => new LuckyPotion(x, y),
                2 => new StrengthPotion(x, y),
                _ => new HealthPotion(x,y),
            };
        }

        public IItem CreateRandomCurrency(int x, int y)
        {
            int currency_type = random.Next(0, 2);
            return currency_type switch
            {
                0 => new Coins(x, y, random.Next(1, 30)),
                1 => new Gold(x, y, random.Next(1, 30)),
                _ => new Coins(x,y,420),
            };
        }

        public IItem CreateRandomItem(int x, int y)
        {
            int item_type = random.Next(0, 4);
            return item_type switch
            {
                0 => CreateRadnomWeapon(x, y),
                1 => CreateRandomCurrency(x, y),
                2 => CreateRandomPotion(x, y),
                3 => CreateRandomModifiedWeapon(x, y),
                _ => CreateRandomItem(x, y),
            };
        }
    }
}
