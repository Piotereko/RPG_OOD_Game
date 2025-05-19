using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Game;
using RPG_wiedzmin_wanna_be.Model.World;

namespace RPG_wiedzmin_wanna_be.Model.Game.ActionHandling
{
    internal class ItemPickUpAction : BasePlayerAction
    {
        public override void HandleAction(ConsoleKey key, Player player, Dungeon dungeon, TurnManager turn_manager)
        {
            if (key == ConsoleKey.E)
            {
                var tile = dungeon.map[player.pos_x, player.pos_y];
                if (tile.items.Count > 0)
                {
                    tile.items[0].PickMe(player);
                    player.inventory.Add(tile.items[0]);
                    tile.items.RemoveAt(0);
                    Logger.PrintLog("Item picked up.");
                }
                else
                {
                    Logger.PrintLog("No items to pick up.");
                }
            }
            else
            {
                base.HandleAction(key, player, dungeon, turn_manager);
            }
        }
    }
}
