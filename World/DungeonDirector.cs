using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.World
{
    internal class DungeonDirector
    {
        private IDungeonBuilder _builder;

        public DungeonDirector(IDungeonBuilder builder)
        {
            _builder = builder;
        }
        public Dungeon CreateTest()
        {
            return _builder.EmptyDungeon()
                           .FilledDungeon()
                           .AddPaths()
                           .AddCentralRoom()
                           .AddChambers()
                           .Build();
                            
        }
    }
}
