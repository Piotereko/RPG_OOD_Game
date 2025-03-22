using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.World
{
    internal interface IDungeonBuilder
    {
        IDungeonBuilder EmptyDungeon();
        IDungeonBuilder FilledDungeon();
        IDungeonBuilder AddPaths();
        IDungeonBuilder AddChambers();
        IDungeonBuilder AddCentralRoom();
        IDungeonBuilder AddItems();
        IDungeonBuilder AddWeapons();
        IDungeonBuilder AddModifiedWeapons();
        IDungeonBuilder AddPotions();
        IDungeonBuilder AddEnemies();
        Dungeon Build();
    }
}
