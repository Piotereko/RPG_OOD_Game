using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Model.World
{
    internal class InstructionBuilder : IDungeonBuilder
    {
        public IDungeonBuilder AddCentralRoom()
        {
            return this;
        }

        public IDungeonBuilder AddChambers()
        {
            return this;
        }

        public IDungeonBuilder AddEnemies()
        {
            return this;
        }

        public IDungeonBuilder AddItems()
        {

            return this;
        }

        public IDungeonBuilder AddModifiedWeapons()
        {
            return this;
        }

        public IDungeonBuilder AddPaths()
        {
            return this;
        }

        public IDungeonBuilder AddPotions()
        {
            return this;
        }

        public IDungeonBuilder AddWeapons()
        {
            return this;
        }

        public Dungeon Build()
        {
            throw new NotImplementedException();
        }

        public IDungeonBuilder EmptyDungeon()
        {
            return this;
        }

        public IDungeonBuilder FilledDungeon()
        {
            return this;
        }
    }
}
