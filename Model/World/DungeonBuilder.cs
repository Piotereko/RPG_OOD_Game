using RPG_wiedzmin_wanna_be.Model.Entity;
using RPG_wiedzmin_wanna_be.Model.Items;
using RPG_wiedzmin_wanna_be.Model.Items.Factory;

namespace RPG_wiedzmin_wanna_be.Model.World
{
    internal class DungeonBuilder : IDungeonBuilder
    {
        private Dungeon dungeon;

        private RandomItemsFactory radnomFactory = new RandomItemsFactory();
        private RandomEnemiesFactory RandomEnemiesFactory = new RandomEnemiesFactory();
        public DungeonBuilder(int height = 20, int width = 40)
        {
            dungeon = new Dungeon(height, width);
        }

        public IDungeonBuilder EmptyDungeon()
        {
            for (int i = 1; i < dungeon.height - 1; i++)
            {
                for (int j = 1; j < dungeon.width - 1; j++)
                {
                    dungeon.map[j][i].IsWall = false;
                }
            }
            return this;

        }

        public IDungeonBuilder FilledDungeon()
        {
            for (int i = 0; i < dungeon.height; i++)
            {
                for (int j = 0; j < dungeon.width; j++)
                {
                    dungeon.map[j][i].IsWall = true;
                }
            }
            return this;

        }
        public IDungeonBuilder AddCentralRoom()
        {
            int room_width = 20;
            int room_height = 10;
            int start_x = dungeon.width / 2 - room_width / 2;
            int start_y = dungeon.height / 2 - room_height / 2;
            for (int y = start_y; y < start_y + room_height; y++)
            {
                for (int x = start_x; x < start_x + room_width; x++)
                {
                    dungeon.map[x][y].IsWall = false;
                }
            }
            return this;

        }

        public IDungeonBuilder AddChambers()
        {
            int chamber_size = 3;

            Random random = new Random();

            int chamberCount = 20;

            int player_x = 1;
            int player_y = 1;
            if (Math.Abs(player_x) < chamber_size + 1 && Math.Abs(player_y) < chamber_size + 1)
            {
                // Place the player's chamber
                dungeon.chambers.Add((player_x, player_y));
                for (int dy = 0; dy < chamber_size; dy++)
                {
                    for (int dx = 0; dx < chamber_size; dx++)
                    {
                        dungeon.map[player_x + dx][player_y + dy].IsWall = false;
                    }
                }
            }

            while (dungeon.chambers.Count < chamberCount)
            {
                int x = random.Next(1, dungeon.width - chamber_size - 1);
                int y = random.Next(1, dungeon.height - chamber_size - 1);

                bool canPlace = true;

                foreach (var (cx, cy) in dungeon.chambers)
                {
                    if (Math.Abs(x - cx) < chamber_size + 1 && Math.Abs(y - cy) < chamber_size + 1)
                    {
                        canPlace = false;
                        break;
                    }
                }

                if (canPlace)
                {
                    dungeon.chambers.Add((x, y));
                    for (int dy = 0; dy < chamber_size; dy++)
                    {
                        for (int dx = 0; dx < chamber_size; dx++)
                        {
                            dungeon.map[x + dx][y + dy].IsWall = false;
                        }
                    }
                }
            }

            return this;
        }

        public IDungeonBuilder AddEnemies()
        {
            Random random = new Random();
            int number_of_enemies = random.Next(2, 8);
            int placed = 0;

            while (placed < number_of_enemies)
            {
                int x = random.Next(1, dungeon.width - 1);
                int y = random.Next(1, dungeon.height - 1);

                if (!dungeon.map[x][y].IsWall)
                {
                    Enemy enemy = RandomEnemiesFactory.CreateRandomEnemy(x, y);
                    dungeon.enemies.Add(enemy);
                    placed++;
                }
            }
            return this;
        }

        public IDungeonBuilder AddItems()
        {
            Random random = new Random();
            int number_of_items = random.Next(50, 70);
            int placed = 0;

            while (placed < number_of_items)
            {
                int x = random.Next(1, dungeon.width - 1);
                int y = random.Next(1, dungeon.height - 1);

                if (!dungeon.map[x][ y].IsWall)
                {
                    IItem item = radnomFactory.CreateRandomItem(x, y);
                    if (item.IsEquipable)
                    {
                        dungeon.HasWeapons = true;
                    }
                    if (item.ItemSign() == 'P')
                    {
                        dungeon.HasPotions = true;
                    }

                    dungeon.AddItem(item);
                    placed++;
                }
            }


            return this;
        }

        public IDungeonBuilder AddModifiedWeapons()
        {
            Random random = new Random();
            int number_of_items = random.Next(2, 8);
            int placed = 0;

            while (placed < number_of_items)
            {
                int x = random.Next(1, dungeon.width - 1);
                int y = random.Next(1, dungeon.height - 1);

                if (!dungeon.map[x][y].IsWall)
                {
                    IItem item = radnomFactory.CreateRandomModifiedWeapon(x, y);
                    dungeon.AddItem(item);
                    placed++;
                }
            }
            dungeon.HasWeapons = true;

            return this;
        }

        public IDungeonBuilder AddPaths()
        {
            Random random = new Random();

            if (dungeon.chambers.Count == 0)
            {
                for (int i = 1; i < dungeon.height - 1; i++)
                {
                    for (int j = 1; j < dungeon.width - 1; j++)
                    {
                        if (random.Next(0, 5) < 3)
                        {
                            dungeon.map[j][i].IsWall = false;
                        }
                    }
                }
            }

            for (int i = 0; i < dungeon.chambers.Count - 1; i++)
            {
                var (startX, startY) = dungeon.chambers[i];
                var (endX, endY) = dungeon.chambers[i + 1];
                int currentX = startX + 3 / 2;
                int currentY = startY + 3 / 2;
                int targetX = endX + 3 / 2;
                int targetY = endY + 3 / 2;
                while (currentX != targetX)
                {
                    if (currentX < targetX)
                        currentX++;
                    else if (currentX > targetX)
                        currentX--;
                    dungeon.map[currentX][currentY].IsWall = false;
                }
                while (currentY != targetY)
                {
                    if (currentY < targetY)
                        currentY++;
                    else if (currentY > targetY)
                        currentY--;
                    dungeon.map[currentX][currentY].IsWall = false;
                }
            }

            return this;
        }



        public IDungeonBuilder AddPotions()
        {
            Random random = new Random();
            int number_of_items = random.Next(10, 30);
            int placed = 0;

            while (placed < number_of_items)
            {
                int x = random.Next(1, dungeon.width - 1);
                int y = random.Next(1, dungeon.height - 1);

                if (!dungeon.map[x][y].IsWall)
                {
                    IItem item = radnomFactory.CreateRandomPotion(x, y);
                    dungeon.AddItem(item);
                    placed++;
                }
            }
            dungeon.HasPotions = true;
            return this;
        }

        public IDungeonBuilder AddWeapons()
        {
            Random random = new Random();
            int number_of_items = random.Next(5, 15);
            int placed = 0;

            while (placed < number_of_items)
            {
                int x = random.Next(1, dungeon.width - 1);
                int y = random.Next(1, dungeon.height - 1);

                if (!dungeon.map[x][y].IsWall)
                {
                    IItem item = radnomFactory.CreateRadnomWeapon(x, y);
                    dungeon.AddItem(item);
                    placed++;
                }
            }

            dungeon.HasWeapons = true;
            return this;
        }

        public Dungeon Build()
        {
            return dungeon;
        }




    }
}
