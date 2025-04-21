using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.World;

namespace RPG_wiedzmin_wanna_be.Game.ActionHandling
{
    internal partial class AttackModeHandler : BasePlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon, TurnManager turn_manager)
        {
            if (key == ConsoleKey.Z)
            {
                Logger.PrintLog("Attack mode changed");
                player.attack_mode += 1;
                player.attack_mode %= 3;
            }
            else
            {
                base.HandleAction(key, player, dungeon, turn_manager);
            }

        }
    }

}
