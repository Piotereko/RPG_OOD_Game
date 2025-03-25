using RPG_wiedzmin_wanna_be.Entity;
using RPG_wiedzmin_wanna_be.Items;
using RPG_wiedzmin_wanna_be.Items.Factory;

namespace RPG_wiedzmin_wanna_be.World
{
    internal class DungeonBuilder
    {
        private Dungeon dungeon;

        private RandomItemsFactory radnomFactory = new RandomItemsFactory();
        private RandomEnemiesFactory RandomEnemiesFactory = new RandomEnemiesFactory();
        public DungeonBuilder(int height = 20, int width = 40)
        {
            dungeon = new Dungeon(height, width);
        }

        public void EmptyDungeon()
        {
            for (int i = 1; i < dungeon.height - 1; i++)
            {
                for (int j = 1; j < dungeon.width - 1; j++)
                {
                    dungeon.map[j, i].IsWall = false;
                }
            }

        }

        public void FilledDungeon()
        {
            for (int i = 0; i < dungeon.height; i++)
            {
                for (int j = 0; j < dungeon.width; j++)
                {
                    dungeon.map[j, i].IsWall = true;
                }
            }

        }
        public void AddCentralRoom()
        {
            int room_width = 20;
            int room_height = 10;
            int start_x = dungeon.width / 2 - room_width / 2;
            int start_y = dungeon.height / 2 - room_height / 2;
            for (int y = start_y; y < start_y + room_height; y++)
            {
                for (int x = start_x; x < start_x + room_width; x++)
                {
                    dungeon.map[x, y].IsWall = false;
                }
            }

        }

        public void AddChambers()
        {
            Random random = new Random();
            for (int i = 1; i < dungeon.height - 1; i++)
            {
                for (int j = 1; j < dungeon.width - 1; j++)
                {
                    if (random.Next(0, 5) == 0)
                    {
                        dungeon.map[j, i].IsWall = false;
                    }
                }
            }

        }

        public void AddEnemies()
        {
            Random random = new Random();
            int number_of_enemies = random.Next(2, 8);
            int placed = 0;

            while (placed < number_of_enemies)
            {
                int x = random.Next(1, dungeon.width - 1);
                int y = random.Next(1, dungeon.height - 1);

                if (!dungeon.map[x, y].IsWall)
                {
                    Enemy enemy = RandomEnemiesFactory.CreateRandomEnemy(x, y);
                    dungeon.enemies.Add(enemy);
                    placed++;
                }
            }
        }

        public void AddItems()
        {
            Random random = new Random();
            int number_of_items = random.Next(50, 70);
            int placed = 0;

            while (placed < number_of_items)
            {
                int x = random.Next(1, dungeon.width - 1);
                int y = random.Next(1, dungeon.height - 1);

                if (!dungeon.map[x, y].IsWall)
                {
                    IItem item = radnomFactory.CreateRandomItem(x, y);
                    /*switch (item.GetType())
                    {
                        case Weapon.GetType():
                            dungeon.HasWeapons = true;
                            break;
                        case Potion.GetType():
                            dungeon.HasPotions = true;
                            break;
                    }
                    if (item.GetType().IsInstanceOfType(o: Weapon)) ;*/
                    if (item.IsEquipable)
                    {
                        dungeon.HasWeapons = true;
                    }
                    dungeon.HasPotions = true;

                    dungeon.AddItem(item);
                    placed++;
                }
            }



        }

        public void AddModifiedWeapons()
        {
            Random random = new Random();
            int number_of_items = random.Next(2, 8);
            int placed = 0;

            while (placed < number_of_items)
            {
                int x = random.Next(1, dungeon.width - 1);
                int y = random.Next(1, dungeon.height - 1);

                if (!dungeon.map[x, y].IsWall)
                {
                    IItem item = radnomFactory.CreateRandomModifiedWeapon(x, y);
                    dungeon.AddItem(item);
                    placed++;
                }
            }
            dungeon.HasWeapons = true;
        }

        public void AddPaths()
        {
            Random random = new Random();
            for (int i = 1; i < dungeon.height - 1; i++)
            {
                for (int j = 1; j < dungeon.width - 1; j++)
                {
                    if (random.Next(0, 5) < 3)
                    {
                        dungeon.map[j, i].IsWall = false;
                    }
                }
            }


            /*  for (int i = 0; i < 15; i++) 
              {

                  int startX = random.Next(1, dungeon.width - 2);
                  int startY = random.Next(1, dungeon.height - 2);


                  int pathLength = random.Next(4, 10);


                  bool isHorizontal = random.Next(0, 2) == 0;


                  for (int j = 0; j < pathLength; j++)
                  {
                      if (isHorizontal && startX + j < dungeon.width)
                      {

                          dungeon.map[startX + j, startY].IsWall = false;
                      }
                      else if (!isHorizontal && startY + j < dungeon.height)
                      {

                          dungeon.map[startX, startY + j].IsWall = true;
                      }
                  }

              }*/

        }

        public void AddPotions()
        {
            Random random = new Random();
            int number_of_items = random.Next(5, 15);
            int placed = 0;

            while (placed < number_of_items)
            {
                int x = random.Next(1, dungeon.width - 1);
                int y = random.Next(1, dungeon.height - 1);

                if (!dungeon.map[x, y].IsWall)
                {
                    IItem item = radnomFactory.CreateRandomPotion(x, y);
                    dungeon.AddItem(item);
                    placed++;
                }
            }
            dungeon.HasPotions = true;
        }

        public void AddWeapons()
        {
            Random random = new Random();
            int number_of_items = random.Next(5, 15);
            int placed = 0;

            while (placed < number_of_items)
            {
                int x = random.Next(1, dungeon.width - 1);
                int y = random.Next(1, dungeon.height - 1);

                if (!dungeon.map[x, y].IsWall)
                {
                    IItem item = radnomFactory.CreateRadnomWeapon(x, y);
                    dungeon.AddItem(item);
                    placed++;
                }
            }

            dungeon.HasWeapons = true;
        }

        public Dungeon Build()
        {
            return dungeon;
        }




    }
}
