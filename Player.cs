using RPG_wiedzmin_wanna_be.Items;
using RPG_wiedzmin_wanna_be.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be
{
    internal class Player
    {
        public int pos_x {  get; set; }
        public int pos_y { get; set; }

        public int strength { get; set; }
        public int dexterity {  get; set; }
        public int health { get; set; }
        public int luck { get; set; }
        public int agression { get; set; }
        public int wisdom {  get; set; }

        public List<IItem> inventory;

        public IItem? RightHand;
        public IItem? LeftHand;
        public bool InInventory = false;
        public int inventory_pos = 0;
        public bool InRightHand = true;

        public Player(int pos_x = 1, int pos_y = 1 , int strength = 10, int dexterity = 10, int health = 10, int luck = 10, int agression = 10, int wisdom = 10)
        {
            this.pos_x = pos_x;
            this.pos_y = pos_y;
            this.strength = strength;
            this.dexterity = dexterity;
            this.health = health;
            this.luck = luck;
            this.agression = agression;
            this.wisdom = wisdom;
            inventory = new List<IItem>();
            


            inventory.Add(new Bow());
            inventory.Add(new Sword());
        }

        public void print_player()
        {
            Console.SetCursorPosition(pos_x, pos_y);
            Console.WriteLine("B");
        }
    }
}
