using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.Items.Weapons;
using RPG_wiedzmin_wanna_be.Model.World;
using RPG_wiedzmin_wanna_be.Visitor_pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RPG_wiedzmin_wanna_be.Model.Entity.Strategy
{
    public class AgressiveBehaviour : IEnemyBehavior
    {
        public void Act(Enemy enemy, Player player, Dungeon dungeon)
        {
            /*int dx = Math.Abs(player.pos_x - enemy.pos_x);
            int dy = Math.Abs( player.pos_y - enemy.pos_y);

            if(dx <= 1 && dy <= 1)
            {
                AttackPlayer(enemy, player);
                //player.health -= enemy.attack_val;
                //Logger.PrintLog($"{enemy} attacked player");
            }
            else
            {
                TryMoveTowardPlayer(enemy, player, dungeon);
            }*/

            if (!Attack(enemy, player, dungeon))
            {
                Move(enemy, player, dungeon);
            }
        }
        private void TryMoveTowardPlayer(Enemy enemy, Player player, Dungeon dungeon ) 
        {
            int new_x = enemy.pos_x + Math.Sign(player.pos_x - enemy.pos_x);
            int new_y = enemy.pos_y + Math.Sign(player.pos_y - enemy.pos_y);

            enemy.TryMove(new_x, new_y,dungeon);
        }

        private void AttackPlayer(Enemy enemy, Player player)
        {
            IDefenseVisitor defenseVisitor = player.attack_mode switch
            {
                0 => new NormalDefenseVisitor(),
                1 => new StealthDefenseVisitor(),
                2 => new MagicDefenseVisitor(),
                _ => new NormalDefenseVisitor()
            };

            int leftDefense = 0;
            if (player.LeftHandChoosed && player.LeftHand != null)
            {
                leftDefense = ((Weapon)player.LeftHand).AcceptDeffence(defenseVisitor, player);
            }

            int rightDefense = 0;
            if (player.RightHandChoosed && player.RightHand != null)
            {
                rightDefense = ((Weapon)player.RightHand).AcceptDeffence(defenseVisitor, player);
            }

            int totalDefense = leftDefense + rightDefense;
            int enemyDamage = Math.Max(1, enemy.attack_val - totalDefense);
            player.health -= enemyDamage;
            Logger.PrintLog($"{enemy.GetType().Name} attacked for {enemyDamage} damage!");
            if (player.health <= 0)
            {
                Logger.PrintLog("You have been defeated!");
                Environment.Exit(0);
            }


        }

        public bool Move(Enemy enemy, Player player, Dungeon dungeon)
        {
            if (enemy.health <= 5)
            {
                RunaAway(enemy, player, dungeon);
            }
            else
            {
                TryMoveTowardPlayer(enemy, player, dungeon);
            }
            return false;

        }

        public bool Attack(Enemy enemy, Player player, Dungeon dungeon)
        {
            int dx = Math.Abs(player.pos_x - enemy.pos_x);
            int dy = Math.Abs(player.pos_y - enemy.pos_y);

            if (dx <= 1 && dy <= 1)
            {
                AttackPlayer(enemy, player);
                return true;
            }
            return false;
        }

        private void RunaAway(Enemy enemy, Player player, Dungeon dungeon)
        {
            int currentDistance = Math.Abs(player.pos_x - enemy.pos_x) + Math.Abs(player.pos_y - enemy.pos_y);

            var possibleMoves = new (int x, int y)[]
            {
            (enemy.pos_x + 1, enemy.pos_y),
            (enemy.pos_x - 1, enemy.pos_y),
            (enemy.pos_x, enemy.pos_y + 1),
            (enemy.pos_x, enemy.pos_y - 1),
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

            enemy.TryMove(bestMove.x, bestMove.y, dungeon);
        }
    }
}
