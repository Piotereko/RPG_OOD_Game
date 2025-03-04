using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items.Currency
{
    internal interface ICurrency:IItem
    {
        public int amount { get; set; }
    }
}
