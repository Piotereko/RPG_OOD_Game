using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be
{
    internal class Player
    {
        int pos_x {  get; set; } = 1;
        int pos_y { get; set; } = 1;

        public int strength { get; set; }
        public int dexterity {  get; set; }
        public int health { get; set; }
        public int luck { get; set; }
        public int agression { get; set; }
        public int wisdom {  get; set; }

        void print_player()
        {
            Console.SetCursorPosition(pos_x, pos_y);
            Console.WriteLine("¶");
        }
    }
}
