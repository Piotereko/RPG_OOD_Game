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
using RPG_wiedzmin_wanna_be.Controller;
using RPG_wiedzmin_wanna_be.DTO.Commands;

namespace RPG_wiedzmin_wanna_be.Model.Game
{
    internal class GameEngine
    {
        private Dungeon dungeon;
        private Player player = new Player(1);
        private DungeonBuilder builder;
        private DungeonDirector director;
        private readonly ConsoleView view = new ConsoleView();
        private TurnManager turnManager;


        private InputHandlerChain inputHandlerChain;
        private ActionExecutor actionExecutor;



        public GameEngine()
        {
            builder = new DungeonBuilder();
            director = new DungeonDirector(builder);
            turnManager = new TurnManager();

            dungeon = builder.Build();
            dungeon = director.CreateTest();

            inputHandlerChain = new InputHandlerChain(dungeon);
            actionExecutor = new ActionExecutor();
            
        }

        public void CreateDungeon()
        {
            dungeon = director.CreateTest();
        }

        



        public void Run()
        {

            Console.Clear();
            Console.CursorVisible = false;

            view.RenderFull(dungeon, player, turnManager);

            while (true)
            {
                view.RenderUpdate(dungeon, player, turnManager);
       
                turnManager.UpdateEffects();


                ConsoleKeyInfo key = Console.ReadKey(true);

                PlayerCommand? command = inputHandlerChain.Handle(key.Key);
                if(command != null)
                {
                    actionExecutor.Execute(command, player, dungeon, turnManager);

                }
                else
                    Logger.PrintLog($"Unhandled key: {key.Key}");


            }
        }
    }

}
