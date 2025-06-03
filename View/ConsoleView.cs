using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.World;
using RPG_wiedzmin_wanna_be.View;
using RPG_wiedzmin_wanna_be.Model;
using RPG_wiedzmin_wanna_be.Model.Game;

namespace RPG_wiedzmin_wanna_be.View
{
    public static class ConsoleView
    {

        public static void RenderUpdate(Dungeon dungeon, Player player, TurnManager turnManager)
        {
            Render.Instance.PrintStats(player);
            Render.Instance.PrintInventory(player);
            Render.Instance.PrintHands(player);
            Render.Instance.PrintTileInfo(dungeon.map[player.pos_x][    player.pos_y]);
            Render.Instance.PrintSteering(dungeon, player);
            //Render.Instance.printEnemies(dungeon);
            Render.Instance.printEnemiesInfo(dungeon, player);
            Render.Instance.PrintActiveEffects(turnManager);
            Render.Instance.PrintLogs();
        }
        public static void RenderFull(Dungeon dungeon, Player player, TurnManager turnManager)
        {
            Render.Instance.printWorld(dungeon);
            Render.Instance.printItems(dungeon);
            Render.Instance.PrintPlayer(player);
            Render.Instance.PrintStats(player);
            Render.Instance.PrintInventory(player);
            Render.Instance.PrintHands(player);
            Render.Instance.PrintTileInfo(dungeon.map[player.pos_x][player.pos_y]);
            Render.Instance.PrintSteering(dungeon, player);
            Render.Instance.printEnemies(dungeon);
            Render.Instance.printEnemiesInfo(dungeon, player);
            Render.Instance.PrintActiveEffects(turnManager);
            Render.Instance.PrintLogs();
        }

        public static void RenderServer(Dungeon dungeon, Dictionary<int, Player> players)
        {
            Render.Instance.printWorld(dungeon);
            Render.Instance.printItems(dungeon);
            Render.Instance.PrintPlayers(players,-1);
        }

        public static void RenderFull(Dungeon dungeon, Dictionary<int, Player> players, int local_player_id,TurnManager turnManager)
        {
        
            if (!players.TryGetValue(local_player_id, out Player player))
            {
                Console.WriteLine($"Player id {local_player_id} not found in players dictionary");
                return;
            }
            
            Render.Instance.printWorld(dungeon);
            Render.Instance.printItems(dungeon);
            
       
            Render.Instance.PrintStats(player);
            Render.Instance.PrintInventory(player);
            Render.Instance.PrintHands(player);
            Render.Instance.PrintTileInfo(dungeon.map[player.pos_x][player.pos_y]);
            Render.Instance.PrintSteering(dungeon, player);
            Render.Instance.printEnemies(dungeon);
            Render.Instance.printEnemiesInfo(dungeon, player);
            Render.Instance.PrintActiveEffects(turnManager);
            Render.Instance.PrintLogs();

            Render.Instance.PrintPlayers(players, local_player_id);
        }

        public static void RenderUpdate(Dungeon dungeon, Dictionary<int, Player> players, int local_player_id ,TurnManager turnManager)
        {
            Player player = players[local_player_id];

            

            Render.Instance.printWorld(dungeon);
            Render.Instance.printItems(dungeon);

            Render.Instance.PrintStats(player);
            Render.Instance.PrintInventory(player);
            Render.Instance.PrintHands(player);
            Render.Instance.PrintTileInfo(dungeon.map[player.pos_x][player.pos_y]);
            Render.Instance.PrintSteering(dungeon, player);
            Render.Instance.printEnemies(dungeon);
            Render.Instance.printEnemiesInfo(dungeon, player);
            Render.Instance.PrintActiveEffects(turnManager);

            Render.Instance.PrintPlayers(players, local_player_id);
            //Render.Instance.PrintLogs();

        }


    }
}
