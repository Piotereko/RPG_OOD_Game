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
        private BasePlayerAction actionHandlerChain;

        private List<Potion> active_potions;

        public GameEngine()
        {
            builder = new DungeonBuilder();
            director = new DungeonDirector(builder);
            active_potions = new List<Potion>();
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
            var invalidInputHandler = new InvalidInputAction();

           
            var currentHandler = moveHandler
                .SetNext(inventoryNavHandler)
                .SetNext(inventorySwitchHandler);

          
            if (dungeon.items.Count > 0)
            {
                var pickUpHandler = new ItemPickUpAction();
                var dropHandler = new ItemDropAction();
                var equipHandler = new EquipAction();

                currentHandler = currentHandler
                    .SetNext(pickUpHandler)
                    .SetNext(dropHandler)
                    .SetNext(equipHandler);
            }

            currentHandler = currentHandler
                .SetNext(exitHandler)
                .SetNext(invalidInputHandler);

            actionHandlerChain = moveHandler;
        }

        public void RegisterPotionEffect(Potion potion)
        {
            active_potions.Add(potion);
        }

        public void RemovePotionEffect(Potion potion)
        {
            active_potions.Remove(potion);
        }

        public void UpdateEffects()
        {
            foreach(var potion in active_potions)
            {
                potion.Update();
                if(potion.Duration ==0)
                {
                    potion.RemoveEffects(player);
                    active_potions.Remove(potion);
                    Logger.PrintLog($"{potion.Name} have expired.");
                }
            }
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
                Render.Instance.PrintActiveEffects(active_potions);
                UpdateEffects();

                ConsoleKeyInfo key = Console.ReadKey(true);
                actionHandlerChain.HandleAction(key.Key, player, dungeon);

            }
        }
    }

}
