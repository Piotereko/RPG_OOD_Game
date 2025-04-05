using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Game.ActionHandling;
using RPG_wiedzmin_wanna_be.Items;
using RPG_wiedzmin_wanna_be.Items.Potions;
using RPG_wiedzmin_wanna_be.Items.Weapons;
using RPG_wiedzmin_wanna_be.World;
using static RPG_wiedzmin_wanna_be.Game.ActionHandling.EquipAction;

namespace RPG_wiedzmin_wanna_be.Game
{
    internal class GameEngine
    {
        private Dungeon dungeon;
        private Player player = new Player();
        private DungeonBuilder builder;
        private DungeonDirector director;
        private PlayerAction actionHandlerChain;

        public GameEngine()
        {
            builder = new DungeonBuilder();
            director = new DungeonDirector(builder);
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
            var pickUpHandler = new ItemPickUpAction();
            var dropHandler = new ItemDropAction();
            var inventoryNavHandler = new InventoryNavigationHandler();
            var inventorySwitchHandler = new InventorySwitchAction();
            var equipHandler = new EquipAction();
            var exitHandler = new ExitHandler();
            var invalidInputHandler = new InvalidInputAction();


            moveHandler
                .SetNext(pickUpHandler)
                .SetNext(dropHandler)
                .SetNext(inventoryNavHandler)
                .SetNext(inventorySwitchHandler)
                .SetNext(equipHandler)
                .SetNext(exitHandler)
                .SetNext(invalidInputHandler);

            actionHandlerChain = moveHandler;

            /* var moveHandler = new MoveAction();
             var inventorySwitchHandler = new InventorySwitchAction();
             var exitHandler = new ExitHandler();
             var invalidInputHandler = new InvalidInputAction();
             PlayerAction currentHandler = moveHandler;
             if (dungeon.items.Count > 0)
             {
                 var pickUpHandler = new ItemPickUpAction();
                 currentHandler.SetNext(pickUpHandler);
             }
             var dropHandler = new ItemDropAction();
             currentHandler.SetNext(dropHandler);
             var inventoryNavHandler = new InventoryNavigationHandler();
             currentHandler.SetNext(inventoryNavHandler);
             currentHandler.SetNext(inventorySwitchHandler);
             if (dungeon.items.Any(item => item.IsEquipable) || player.inventory.Count > 0)
             {
                 var equipHandler = new EquipAction();
                 currentHandler .SetNext(equipHandler);
             }
              currentHandler.SetNext(exitHandler).SetNext(invalidInputHandler);

             actionHandlerChain = moveHandler;*/
        }

        public void Run()
        {
           // CreateDungeon();

            Console.Clear();
            Console.CursorVisible = false;
            Render.Instance.printWorld(dungeon);
            Render.Instance.printItems(dungeon);
            Render.Instance.PrintPlayer(player);
           

            while (true)
            {
                Render.Instance.PrintStats(player);
                Render.Instance.PrintInventory(player);
                Render.Instance.PrintTileInfo(dungeon.map[player.pos_x, player.pos_y]);
                Render.Instance.PrintHands(player);
                Render.Instance.PrintSteering(dungeon, player);
                Render.Instance.printEnemies(dungeon);
                Render.Instance.printEnemiesInfo(dungeon,player);

                ConsoleKeyInfo key = Console.ReadKey(true);
                actionHandlerChain.HandleAction(key.Key, player, dungeon);

            }
        }
    }

}
