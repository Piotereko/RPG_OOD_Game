namespace RPG_wiedzmin_wanna_be.World
{
    internal interface IDungeonBuilder
    {
        IDungeonBuilder AddCentralRoom();
        IDungeonBuilder AddChambers();
        IDungeonBuilder AddEnemies();
        IDungeonBuilder AddItems();
        IDungeonBuilder AddModifiedWeapons();
        IDungeonBuilder AddPaths();
        IDungeonBuilder AddPotions();
        IDungeonBuilder AddWeapons();
        
        IDungeonBuilder EmptyDungeon();
        IDungeonBuilder FilledDungeon();
        Dungeon Build();
    }
}