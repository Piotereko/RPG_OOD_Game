using RPG_wiedzmin_wanna_be.Model.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.Entity.Strategy
{
    public class SkittishBehaviour : IEnemyBehavior
    {
        public void Act(Enemy enemy, Player player, Dungeon dungeon)
        {
            int currentDistance = Math.Abs(player.pos_x - enemy.pos_x) + Math.Abs(player.pos_y - enemy.pos_y);

            var possibleMoves = new (int x, int y)[]
            {
            (enemy.pos_x + 1, enemy.pos_y),
            (enemy.pos_x - 1, enemy.pos_y),
            (enemy.pos_x, enemy.pos_y + 1),
            (enemy.pos_x, enemy.pos_y - 1),
            //(enemy.pos_x + 1, enemy.pos_y + 1),
            //(enemy.pos_x + 1, enemy.pos_y - 1),
            //(enemy.pos_x - 1, enemy.pos_y + 1),
            //(enemy.pos_x - 1, enemy.pos_y - 1),
            (enemy.pos_x, enemy.pos_y)
            };

            (int x, int y) bestMove = (enemy.pos_x, enemy.pos_y);
            int bestDistance = currentDistance;

            foreach (var move in possibleMoves)
            {
                if (dungeon.map[move.x][move.y].IsWall)
                    continue;

                int moveDistance = Math.Abs(player.pos_x - move.x) + Math.Abs(player.pos_y - move.y);

                if (moveDistance > bestDistance)
                {
                    bestDistance = moveDistance;
                    bestMove = move;
                }
            }

            //enemy.pos_x = bestMove.x;
           // enemy.pos_y = bestMove.y;

            enemy.TryMove(bestMove.x, bestMove.y,dungeon);
        }
    }
}
