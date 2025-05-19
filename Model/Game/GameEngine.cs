using RPG_wiedzmin_wanna_be.Game.ActionHandling;
using RPG_wiedzmin_wanna_be.Model.Items;
using RPG_wiedzmin_wanna_be.Model.Items.Potions;
using RPG_wiedzmin_wanna_be.Model.Items.Weapons;
using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.World;
using RPG_wiedzmin_wanna_be.View;
using static RPG_wiedzmin_wanna_be.Game.ActionHandling.AttackModeHandler;
using static RPG_wiedzmin_wanna_be.Model.Game.ActionHandling.EquipAction;
using RPG_wiedzmin_wanna_be.Model.Game.ActionHandling;

namespace RPG_wiedzmin_wanna_be.Model.Game
{
    internal class GameEngine
    {
        private Dungeon dungeon;
        private Player player = new Player();
        private DungeonBuilder builder;
        private DungeonDirector director;
        private BasePlayerAction actionHandlerChain;
        private readonly ConsoleView view = new ConsoleView();

        private TurnManager turnManager;



        public GameEngine()
        {
            builder = new DungeonBuilder();
            director = new DungeonDirector(builder);
            turnManager = new TurnManager();

            dungeon = builder.Build();
            dungeon = director.CreateTest();
            InitializeActionHandlers();
        }

        public void CreateDungeon()
        {
            dungeon = director.CreateTest();
        }

        private void InitializeActionHandlers()
        {
            var moveHandler = new MoveAction();
            var inventoryNavHandler = new InventoryNavigationHandler();
            var inventorySwitchHandler = new InventorySwitchAction();
            var exitHandler = new ExitHandler();
            var attackmode = new AttackModeHandler();
            var attackHandler = new AttackAction();



            var currentHandler = moveHandler
                .SetNext(inventoryNavHandler)
                .SetNext(inventorySwitchHandler)
                .SetNext(attackHandler)
                .SetNext(attackmode);


            if (dungeon.items.Count > 0)
            {
                var pickUpHandler = new ItemPickUpAction();
                var dropHandler = new ItemDropAction();
                var equipHandler = new EquipAction();

                currentHandler
                    .SetNext(pickUpHandler)
                    .SetNext(dropHandler)
                    .SetNext(equipHandler);
            }

            currentHandler
                .SetNext(exitHandler);


            actionHandlerChain = moveHandler;
        }



        public void Run()
        {

            Console.Clear();
            Console.CursorVisible = false;
            /*            Render.Instance.printWorld(dungeon);
                        Render.Instance.printItems(dungeon);
                        Render.Instance.PrintPlayer(player);*/
            view.RenderFull(dungeon, player, turnManager);

            while (true)
            {
                view.RenderUpdate(dungeon, player, turnManager);
                /*                Render.Instance.PrintStats(player);
                                Render.Instance.PrintInventory(player);
                                Render.Instance.PrintTileInfo(dungeon.map[player.pos_x, player.pos_y]);
                                Render.Instance.PrintHands(player);
                                Render.Instance.PrintSteering(dungeon, player);
                                Render.Instance.printEnemies(dungeon);
                                Render.Instance.printEnemiesInfo(dungeon,player);
                                Render.Instance.PrintActiveEffects(turnManager);
                                Render.Instance.PrintLogs();*/
                turnManager.UpdateEffects();


                ConsoleKeyInfo key = Console.ReadKey(true);
                actionHandlerChain.HandleAction(key.Key, player, dungeon, turnManager);

            }
        }
    }

}
