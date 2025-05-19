using System;
using RPG_wiedzmin_wanna_be.DTO.Commands;
using RPG_wiedzmin_wanna_be.DTO;
using RPG_wiedzmin_wanna_be.Game.ActionHandling;
using RPG_wiedzmin_wanna_be.Model.World;

namespace RPG_wiedzmin_wanna_be.Controller
{
    public class InputHandlerChain
    {
        private BaseInputHandler root;

        public InputHandlerChain(Dungeon dungeon)
        {
            root = InitializeHandlers(dungeon);
        }

        private BaseInputHandler InitializeHandlers(Dungeon dungeon)
        {
            var moveHandler = new MoveInputHandler();
            var inventoryNavHandler = new InventoryNavigationHandler();
            var inventorySwitchHandler = new InventorySwitchHandler();
            var attackHandler = new AttackInputHandler();
            var attackModeHandler = new AttackModeHandler();

            var currentHandler = moveHandler
                .SetNext(inventoryNavHandler)
                .SetNext(inventorySwitchHandler)
                .SetNext(attackHandler)
                .SetNext(attackModeHandler);

            if (dungeon.items.Count > 0)
            {
                var pickUpHandler = new PickupInputHandler();
                var dropHandler = new DropInputHandler();
                var equipHandler = new EquipInputHandler();

                currentHandler
                    .SetNext(pickUpHandler)
                    .SetNext(dropHandler)
                    .SetNext(equipHandler);
            }

            var exitHandler = new ExitInputHandler();
            currentHandler.SetNext(exitHandler);

            return moveHandler;
        }

        public PlayerCommand? Handle(ConsoleKey key)
        {
            return root.Handle(key);
        }
    }
}