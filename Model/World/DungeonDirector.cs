using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.World
{
    internal class DungeonDirector
    {
        private DungeonBuilder builder;

        public DungeonDirector(DungeonBuilder _builder)
        {
            builder = _builder;
        }

        public Dungeon BuildBasicDungeon()
        {
            return builder.EmptyDungeon().FilledDungeon().Build();
        }
        public Dungeon CreateTest()
        {
            return builder.FilledDungeon()
                           .AddChambers()
                           .AddCentralRoom()
                           .AddPaths()
                           .AddItems()
                           .AddPotions()
                           .AddEnemies()
                           .Build();
        }


    }
}
