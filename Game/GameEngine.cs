using RPG_wiedzmin_wanna_be.Items;

namespace RPG_wiedzmin_wanna_be.Game
{
    internal class GameEngine
    {
        World world = new World();
        Player player = new Player(); 
        public bool TryPickUp()
        {
            Tile tile = world.map[player.pos_x, player.pos_y];
            if (tile.items != null && tile.items.Count > 0)
            {
                tile.items[0].PickMe(player);
                player.inventory.Add(tile.items[0]);
                tile.items.RemoveAt(0);
                return true;
            }
            return false;
        }

        public bool DropItem()
        {
            if (player.InInventory == true)
            {
                if (player.inventory.Count > 0)
                {
                    Tile tile = world.map[player.pos_x, player.pos_y];
                    IItem item = player.inventory[player.inventory_pos]; //item to drop

                    item.X_position = player.pos_x; item.Y_position = player.pos_y;
                    item.DropMe(player);

                    tile.items.Add(item);
                    player.inventory.RemoveAt(player.inventory_pos);

                    if (player.inventory_pos == player.inventory.Count) //if we drop last item from inventowy we need to go to previous one
                        player.inventory_pos--;

                    return true;
                }
                else
                    Logger.PrintLog("no item to drop");
            }
            return false;
        }

        public void TryEuipItem()
        {
            if (player.inventory.Count == 0)
            {
                Logger.PrintLog("no items to equip");
                return;
            }

            IItem item = player.inventory[player.inventory_pos];

            if (!item.IsEquipable)
            {
                Logger.PrintLog($"{item} cannot be equipped");
                return;
            }

            if (item.IsTwoHanded)
            {
                if (player.RightHand != null || player.LeftHand != null)
                {
                    Logger.PrintLog("Cannot equip: Hands are occupied. ");
                    return;
                }
                player.RightHand = item;
                player.LeftHand = item;

            }
            else
            {
                if (player.InRightHand)
                {
                    if (player.RightHand == null)
                        player.RightHand = item;
                    else
                    {
                        Logger.PrintLog("Right hand occupied");
                        return;
                    }

                }
                else
                {
                    if (player.LeftHand == null)
                        player.LeftHand = item;
                    else
                    {
                        Logger.PrintLog("left hand occupied");
                        return;
                    }
                }
            }
            if (player.inventory_pos == player.inventory.Count)
            {
                player.inventory_pos--;
            }
            player.ApplyEffect(item);
            player.inventory.Remove(item);
            Logger.PrintLog($"{item} equipped");
        }


        public void Unequip()
        {

            if (player.InRightHand == true)
            {
                if (player.RightHand == null)
                {
                    Logger.PrintLog("right hand is empty");
                    return;
                }
                else
                {
                    player.inventory.Add(player.RightHand);
                    player.RemoveEffect(player.RightHand);
                    if (player.RightHand.IsTwoHanded == true)
                        player.LeftHand = null;
                    player.RightHand = null;
                }
            }
            else
            {
                if (player.LeftHand == null)
                {
                    Logger.PrintLog("left hand is empty");
                    return;
                }
                else
                {
                    player.RemoveEffect(player.LeftHand);
                    player.inventory.Add(player.LeftHand);
                    if (player.LeftHand.IsTwoHanded == true)
                        player.RightHand = null;
                    player.LeftHand = null;
                }
            }
        }

        public void TryWalk(int new_x, int new_y)
        {
            Logger.PrintLog("Trying to move to " + new_x + "," + new_y);
            if (world.map[new_x, new_y].IsWall)
            {
                Logger.PrintLog("Unable to move: Wall");
                return;
            }

            Render.printTile(world.map[player.pos_x, player.pos_y]);
            player.pos_x = new_x;
            player.pos_y = new_y;
            Render.PrintPlayer(player);
            return;
        }


        public void Run()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Render.printWorld(world);
            Render.printItems(world);
            Render.PrintPlayer(player);
            Render.PrintSteering();

            while (true)
            {
                Render.PrintStats(player);
                Render.PrintInventory(player);
                Render.PrintTileInfo(world.map[player.pos_x, player.pos_y]);
                Render.PrintHands(player);

                ConsoleKeyInfo key = Console.ReadKey(true);


                switch (key.Key)
                {
                    case ConsoleKey.W:
                        //move down
                        if (player.InInventory == true)
                        {
                            if (player.inventory_pos > 0)
                                player.inventory_pos--;
                            else
                                player.inventory_pos = player.inventory.Count - 1;
                        }
                        else
                            TryWalk(player.pos_x, player.pos_y - 1);
                        break;


                    case ConsoleKey.S:
                        //move up
                        if (player.InInventory == true)
                        {
                            if (player.inventory_pos < player.inventory.Count - 1)
                                player.inventory_pos++;
                            else
                                player.inventory_pos = 0;
                        }
                        else
                            TryWalk(player.pos_x, player.pos_y + 1);
                        break;


                    case ConsoleKey.D:
                        //move right
                        if (player.InInventory == false)
                            TryWalk(player.pos_x + 1, player.pos_y);
                        break;


                    case ConsoleKey.A:
                        if (player.InInventory == false)
                            TryWalk(player.pos_x - 1, player.pos_y);
                        //move left
                        break;


                    case ConsoleKey.E:
                        if (player.InInventory == false)
                        {
                            if (TryPickUp())
                                Logger.PrintLog("item picked");
                            else
                                Logger.PrintLog("unable to pick up");
                        }
                        else
                        {
                            if (DropItem())
                                Logger.PrintLog("item dropped");
                            else
                                Logger.PrintLog("bombing failed");
                        }
                        break;


                    case ConsoleKey.F:
                        if (player.InInventory == false)
                        {
                            Logger.PrintLog("unequping item");
                            Unequip();
                        }
                        else
                        {
                            Logger.PrintLog("equping item");
                            TryEuipItem();
                        }
                        break;


                    case ConsoleKey.Q:
                        if (player.InInventory == false)
                        {
                            player.InInventory = true;
                            Logger.PrintLog("switch to inventory");
                        }
                        else
                        {
                            player.InInventory = false;
                            player.inventory_pos = 0;
                            Logger.PrintLog("switch out of inventory");
                        }
                        break;


                    case ConsoleKey.R:
                        //changing hand
                        Logger.PrintLog("changing hands");
                        if (player.InRightHand)
                        {
                            player.InRightHand = false;
                        }
                        else
                            player.InRightHand = true;
                        break;


                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }

}
