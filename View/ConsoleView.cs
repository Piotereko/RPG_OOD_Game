using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.World;
using RPG_wiedzmin_wanna_be.View;
using RPG_wiedzmin_wanna_be.Model;
using RPG_wiedzmin_wanna_be.Model.Game;

namespace RPG_wiedzmin_wanna_be.View
{
    public class ConsoleView
    {

        public void RenderUpdate(Dungeon dungeon, Player player, TurnManager turnManager)
        {
            Render.Instance.PrintStats(player);
            Render.Instance.PrintInventory(player);
            Render.Instance.PrintHands(player);
            Render.Instance.PrintTileInfo(dungeon.map[player.pos_x, player.pos_y]);
            Render.Instance.PrintSteering(dungeon, player);
            Render.Instance.printEnemies(dungeon);
            Render.Instance.printEnemiesInfo(dungeon, player);
            Render.Instance.PrintActiveEffects(turnManager);
            Render.Instance.PrintLogs();
        }
        public void RenderFull(Dungeon dungeon, Player player, TurnManager turnManager)
        {
            Render.Instance.printWorld(dungeon);
            Render.Instance.printItems(dungeon);
            Render.Instance.PrintPlayer(player);
            Render.Instance.PrintStats(player);
            Render.Instance.PrintInventory(player);
            Render.Instance.PrintHands(player);
            Render.Instance.PrintTileInfo(dungeon.map[player.pos_x, player.pos_y]);
            Render.Instance.PrintSteering(dungeon, player);
            Render.Instance.printEnemies(dungeon);
            Render.Instance.printEnemiesInfo(dungeon, player);
            Render.Instance.PrintActiveEffects(turnManager);
            Render.Instance.PrintLogs();
        }
    }
}
