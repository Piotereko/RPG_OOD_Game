using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items.Weapons;
using RPG_wiedzmin_wanna_be.Visitor_pattern;
using RPG_wiedzmin_wanna_be.World;

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
                    .Where(e => (Math.Abs(e.pos_x - player.pos_x) <= 1 && Math.Abs(e.pos_y - player.pos_y) <= 1))
                    .ToList();

                if (!nerbyenemies.Any())
                {
                    Logger.PrintLog("No enemies nearby to attack!");
                    return;
                }

                
                var enemy = nerbyenemies.First();
                FightEnemy(player,enemy);
                
                dungeon.enemies.RemoveAll(e => !e.IsAlive);
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

               
                int leftDamage = player.LeftHand is Weapon leftWeapon
                    ? leftWeapon.AcceptAttack(attackVisitor, player)
                    : 0;

                int rightDamage = player.RightHand is Weapon rightWeapon
                    ? rightWeapon.AcceptAttack(attackVisitor, player)
                    : 0;

                int totalDamage = leftDamage + rightDamage;

                
                //with {GetAttackModeName(player)}
                enemy.TakeDamage(totalDamage);

                Logger.PrintLog($"You attacked {enemy.GetType().Name} for {totalDamage} damage!");

                if (enemy.health <= 0)
                {
                    Logger.PrintLog($"You defeated {enemy.GetType().Name}!");
                    
                    return;
                }

                
                int leftDefense = player.LeftHand is Weapon leftWeaponDef
                    ? leftWeaponDef.AcceptDeffence(defenseVisitor, player)
                    : 0;

                int rightDefense = player.RightHand is Weapon rightWeaponDef
                    ? rightWeaponDef.AcceptDeffence(defenseVisitor, player)
                    : 0;

                int totalDefense = leftDefense + rightDefense;

                int enemyDamage = Math.Max(1, enemy.attack_val - totalDefense);
                player.health -= enemyDamage;
                Logger.PrintLog($"{enemy.GetType().Name} counterattacked for {enemyDamage} damage!");

                if (!enemy.IsAlive)
                {
                    Logger.PrintLog("You have been defeated!");
                    Environment.Exit(0);
                }
            }

            private string GetAttackModeName(Player player)
            {
                return player.attack_mode switch
                {
                    0 => "Normal Attack",
                    1 => "Stealth Attack",
                    2 => "Magic Attack",
                    _ => "Unknown Attack"
                };
            }
        }
    }

}
