using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.DTO.Commands
{
    public class PlayerCommand
    {
        public int PlayerId { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Parameters { get; set; } = new();
    }

    public class AttackModeSwitchCommand : PlayerCommand
    {
        public AttackModeSwitchCommand()
        {
            Type = "attackmodeswitch";
        }
    }

    public class AttackCommand : PlayerCommand
    {
        public AttackCommand()
        {
            Type = "attack";
        }
    }

    public class ExitCommand : PlayerCommand
    {
        public ExitCommand()
        {
            Type = "exit";
        }
    }

    public class InventoryNavigateCommand : PlayerCommand
    {
        public InventoryNavigateCommand(string directionOrHand)
        {
            Type = "inventorynavigate";
            Parameters["target"] = directionOrHand; // "left", "right", "select-left", etc.
        }
    }

    public class InventorySwitchCommand : PlayerCommand
    {
        public InventorySwitchCommand()
        {
            Type = "inventoryswitch";
        }
    }

    public class DropCommand : PlayerCommand
    {
        public DropCommand(string mode)
        {
            Type = "drop";
            Parameters["mode"] = mode; // "inventory", "hands", "all"
        }
    }

    public class MoveCommand : PlayerCommand
    {
        public MoveCommand(string direction)
        {
            Type = "move";
            Parameters["direction"] = direction;
        }
    }

    public class PickupCommand : PlayerCommand
    {
        public PickupCommand()
        {
            Type = "pickup";
        }
    }

    public class EquipCommand : PlayerCommand
    {
   
        public EquipCommand()
        {
            Type = "equip";
           
        }
    }
}
