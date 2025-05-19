using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.Game.ActionHandling;
using RPG_wiedzmin_wanna_be.Model.Items.Weapons;
using RPG_wiedzmin_wanna_be.Model.World;
using RPG_wiedzmin_wanna_be.Visitor_pattern;

namespace RPG_wiedzmin_wanna_be.Game.ActionHandling
{
    internal partial class AttackModeHandler
    {
        internal class AttackAction : BasePlayerAction
        {
            public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon, TurnManager turnManager)
            {
                if (key != ConsoleKey.X)
                {
                    base.HandleAction(key, player, dungeon, turnManager);
                    return;
                }

                
                var nerbyenemies = dungeon.enemies
                    .Where(e => (Math.Abs(e.pos_x - player.pos_x) <= 1 && Math.Abs(e.pos_y - player.pos_y) <= 1) && e.IsAlive)
                    .ToList();

                if (!nerbyenemies.Any())
                {
                    Logger.PrintLog("No enemies nearby to attack!");
                    return;
                }

                
                var enemy = nerbyenemies.First();
                FightEnemy(player,enemy);
                
                //dungeon.enemies.RemoveAll(e => !e.IsAlive);
            }


            private void FightEnemy(Player player,Enemy enemy)
            {
                if (enemy == null || !enemy.IsAlive) return;

                
                IAttackVisitor attackVisitor = player.attack_mode switch
                {
                    0 => new NormalAttackVisitor(),
                    1 => new StealthAttackVisitor(),
                    2 => new MagicAttackVisitor(),
                    _ => new NormalAttackVisitor()
                };

                IDefenseVisitor defenseVisitor = player.attack_mode switch
                {
                    0 => new NormalDefenseVisitor(),
                    1 => new StealthDefenseVisitor(),
                    2 => new MagicDefenseVisitor(),
                    _ => new NormalDefenseVisitor()
                };

                int right_damage = 0;
                if(player.RightHandChoosed)
                {
                    if(player.RightHand != null)
                    {
                        right_damage = ((Weapon)player.RightHand).AcceptAttack(attackVisitor, player);
                    }
                }
                int left_damage = 0;
                if(player.LeftHandChoosed)
                {
                    if (player.LeftHand != null)
                    {
                        left_damage = ((Weapon)player.LeftHand).AcceptAttack(attackVisitor, player);
                    }
                }
                int total_dmg = left_damage+ right_damage;
                if(player.LeftHandChoosed && player.RightHandChoosed && player.RightHand != null)
                {
                    if(player.RightHand.IsTwoHanded)
                    {
                        total_dmg /= 2;
                    }
                }

               
                enemy.TakeDamage(total_dmg);

                Logger.PrintLog($"You attacked {enemy.GetType().Name} for {total_dmg} damage!");

                if (enemy.health <= 0)
                {
                    Logger.PrintLog($"You defeated {enemy.GetType().Name}!");
                    
                    return;
                }



                int leftDefense = 0;
                if (player.LeftHandChoosed)
                {
                    if (player.LeftHand != null)
                    {
                        leftDefense = ((Weapon)player.LeftHand).AcceptDeffence(defenseVisitor, player);
                    }
                }

                int rightDefense = 0;
                if (player.RightHandChoosed)
                {
                    if (player.RightHand != null)
                    {
                        rightDefense = ((Weapon)player.RightHand).AcceptDeffence(defenseVisitor, player);
                    }
                }

                int totalDefense = leftDefense + rightDefense;

                int enemyDamage = Math.Max(1, enemy.attack_val - totalDefense);
                player.health -= enemyDamage;
                Logger.PrintLog($"{enemy.GetType().Name} counterattacked for {enemyDamage} damage!");

                if (player.health <= 0)
                {
                    Logger.PrintLog("You have been defeated!");
                    Environment.Exit(0);
                }
            }
        }
    }

}
