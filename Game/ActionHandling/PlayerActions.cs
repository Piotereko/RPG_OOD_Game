using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.World;

namespace RPG_wiedzmin_wanna_be.Game.ActionHandling
{
    internal abstract class BasePlayerAction : IPlayerAction
    {

        private IPlayerAction? next_handler;
        public IPlayerAction SetNext(IPlayerAction action)
        {
            if (next_handler != null)
            {
                next_handler.SetNext(action);
            }
            else
            {
                next_handler = action;
            }
            return action;
        }

        public virtual void HandleAction(ConsoleKey key, Player player, Dungeon dungeo,TurnManager turn_manager) 
        {
            if (next_handler != null)
            {
                next_handler.HandleAction(key, player, dungeo,turn_manager);
            }
            else
            {
                Logger.PrintLog($"Invalid input. {key} is not supported!");
            }
        }
    }
}
