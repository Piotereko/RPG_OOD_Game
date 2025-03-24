using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.World
{
    internal interface IDungeonBuilder
    {
        void EmptyDungeon();
        void FilledDungeon();
        void AddPaths();
        void AddChambers();
        void AddCentralRoom();
        void AddItems();
        void AddWeapons();
        void AddModifiedWeapons();
        void AddPotions();
        void AddEnemies();
        //Dungeon Build();
    }
}
