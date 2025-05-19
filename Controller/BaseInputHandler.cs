using RPG_wiedzmin_wanna_be.DTO.Commands;

namespace RPG_wiedzmin_wanna_be.Controller
{
    public abstract class BaseInputHandler
    {
        private BaseInputHandler? next;

        public BaseInputHandler SetNext(BaseInputHandler handler)
        {
            if (next != null)
            {
                next.SetNext(handler);
            }
            else
            {
                next = handler;
            }
            return handler;
        }

        public virtual PlayerCommand? Handle(ConsoleKey Key)
        {
            return next?.Handle(Key);
        }
    }

    public class AttackModeHandler : BaseInputHandler
    {
        public override PlayerCommand? Handle(ConsoleKey key)
        {
            if (key == ConsoleKey.C)
            {
                /*Logger.PrintLog("Attack mode changed");
                player.attack_mode += 1;
                player.attack_mode %= 3;*/
                return new AttackModeSwitchCommand();
            }

            return base.Handle(key);
        }
    }

    public class InventoryNavigationHandler : BaseInputHandler
    {
        public override PlayerCommand? Handle(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.D1 => new InventoryNavigateCommand("left"),
                ConsoleKey.D2 => new InventoryNavigateCommand("right"),
                _ => base.Handle(key)
            };
        }
    }

    public class InventorySwitchHandler : BaseInputHandler
    {
        public override PlayerCommand? Handle(ConsoleKey key)
        {
            if (key == ConsoleKey.R)
                return new InventorySwitchCommand();

            return base.Handle(key);
        }
    }

    public class DropInputHandler : BaseInputHandler
    {
        public override PlayerCommand? Handle(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.Q => new DropCommand("single"),
                ConsoleKey.Z => new DropCommand("all"),
                _ => base.Handle(key)
            };
        }
    }

    public class ExitInputHandler : BaseInputHandler
    {
        public override PlayerCommand? Handle(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape)
                return new ExitCommand();

            return base.Handle(key);
        }
    }

    public class PickupInputHandler : BaseInputHandler
    {
        public override PlayerCommand? Handle(ConsoleKey key)
        {
            if (key == ConsoleKey.E)
                return new PickupCommand();

            return base.Handle(key);
        }
    }

    public class AttackInputHandler : BaseInputHandler
    {
        public override PlayerCommand? Handle(ConsoleKey key)
        {
            if (key == ConsoleKey.X)
                return new AttackCommand();

            return base.Handle(key);
        }
    }

    public class EquipInputHandler : BaseInputHandler
    {
        public override PlayerCommand? Handle(ConsoleKey key)
        {
            if (key == ConsoleKey.F)
                return new EquipCommand(); 

            return base.Handle(key);
        }
    }

    public class MoveInputHandler : BaseInputHandler
    {
        public override PlayerCommand? Handle(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.W => new MoveCommand("up"),
                ConsoleKey.S => new MoveCommand("down"),
                ConsoleKey.A => new MoveCommand("left"),
                ConsoleKey.D => new MoveCommand("right"),
                _ => base.Handle(key)
            };
        }
    }










}


