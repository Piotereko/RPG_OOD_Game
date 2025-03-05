using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Items
{
    internal interface IItem
    {
        bool IsUsable { get; }
        int X_position { get; set; }
        int Y_position {  get; set; }
    }
}
