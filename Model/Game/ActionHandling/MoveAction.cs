using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.World;
using RPG_wiedzmin_wanna_be.View;

namespace RPG_wiedzmin_wanna_be.Model.Game.ActionHandling
{
    internal class MoveAction : BasePlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon, TurnManager turn_manager)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    TryWalk(player.pos_x, player.pos_y - 1, player, dungeon);
                    break;
                case ConsoleKey.S:
                    TryWalk(player.pos_x, player.pos_y + 1, player, dungeon);
                    break;
                case ConsoleKey.A:
                    TryWalk(player.pos_x - 1, player.pos_y, player, dungeon);
                    break;
                case ConsoleKey.D:
                    TryWalk(player.pos_x + 1, player.pos_y, player, dungeon);
                    break;
                default:
                    base.HandleAction(key, player, dungeon, turn_manager);
                    break;

            };
        }

        private void TryWalk(int new_x, int new_y, Player player, Dungeon dungeon)
        {
            if (!dungeon.map[new_x, new_y].IsWall)
            {
                Render.Instance.printTile(dungeon.map[player.pos_x, player.pos_y]);
                player.pos_x = new_x;
                player.pos_y = new_y;
                Render.Instance.PrintPlayer(player);
            }
            else
            {
                Logger.PrintLog("Unable to move: Wall");
            }
        }
    }
}
