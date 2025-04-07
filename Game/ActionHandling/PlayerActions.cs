using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.World;

namespace RPG_wiedzmin_wanna_be.Game.ActionHandling
{
    internal abstract class BasePlayerAction : IPlayerAction
    {

        private IPlayerAction next_handler;
        public IPlayerAction SetNext(IPlayerAction action)
        {
            next_handler = action;
            return action;
        }

        public virtual void HandleAction(ConsoleKey key, Player player, Dungeon dungeon)
        {
            if (next_handler != null)
            {
                next_handler.HandleAction(key, player, dungeon);
            }
        }
    }
}
