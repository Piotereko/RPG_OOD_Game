using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.World
{
    internal class DungeonDirector
    {
        private DungeonBuilder builder;

        public DungeonDirector(DungeonBuilder _builder)
        {
            builder = _builder;
        }

        public void BuildBasicDungeon()
        {
            builder.EmptyDungeon().FilledDungeon();
            
                            
        }
        public Dungeon CreateTest()
        {
            BuildBasicDungeon();
            
            builder.AddPaths();
            builder.AddCentralRoom();
            builder.AddChambers();
            builder.AddItems();
            builder.AddEnemies();
            
            return builder.Build();

            
              
        }
    }
}
