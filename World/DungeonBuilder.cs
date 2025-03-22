using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.World
{
    internal class DungeonBuilder : IDungeonBuilder
    {
        private Dungeon dungeon;
        public DungeonBuilder(int height = 20, int width = 40) 
        {
            dungeon = new Dungeon(height, width);
        }
        public IDungeonBuilder AddCentralRoom()
        {
            int room_width = 20;
            int room_height = 10;
            int start_x = dungeon.width /2 - room_width / 2;
            int start_y =dungeon.height /2 - room_height / 2;
            for(int y = start_y; y < start_y + room_height; y++ )
            {
                for(int x = start_x; x < start_x + room_width;x++)
                {
                    dungeon.map[x, y].IsWall = false;
                }
            }
            return this;
        }

        public IDungeonBuilder AddChambers()
        {
            Random random = new Random();
            for(int i = 1; i < dungeon.height-1;i++)
            {
                for(int j = 1; j < dungeon.width - 1;j++)
                {
                    if(random.Next(0,5) == 0)
                    {
                        dungeon.map[j, i].IsWall = false;
                    }
                }
            }
            return this;
        }

        public IDungeonBuilder AddEnemies()
        {
            throw new NotImplementedException();
        }

        public IDungeonBuilder AddItems()
        {
            throw new NotImplementedException();
        }

        public IDungeonBuilder AddModifiedWeapons()
        {
            throw new NotImplementedException();
        }

        public IDungeonBuilder AddPaths()
        {
            /*Random random = new Random();
            for(int i = 1; i < dungeon.height-1;i++)
            {
                for(int j = 1; j < dungeon.width-1;j++)
                {
                    if(random.Next(0,5) < 3)
                    {
                        dungeon.map[j, i].IsWall = false;
                    }
                }
            }*/
            Random random = new Random();

          
            for (int i = 0; i < 15; i++) 
            {
               
                int startX = random.Next(1, dungeon.width - 2);
                int startY = random.Next(1, dungeon.height - 2);

                
                int pathLength = random.Next(4, 8);

               
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
               
            }
            return this;
        }

        public IDungeonBuilder AddPotions()
        {
            throw new NotImplementedException();
        }

        public IDungeonBuilder AddWeapons()
        {
            throw new NotImplementedException();
        }

        public Dungeon Build()
        {
            return dungeon;
        }

        public IDungeonBuilder EmptyDungeon()
        {
            for(int i = 1; i < dungeon.height-1; i++)
            {
                for(int j = 1; j < dungeon.width-1; j++)
                {
                    dungeon.map[j, i].IsWall = false;
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
                    dungeon.map[j, i].IsWall = true;
                }
            }
            return this;
        }
    }
}
