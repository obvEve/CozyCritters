LootJS.lootTables(event => {
    event.forEachTable(table => {
        // disable the normal output loots
        global.disabledOutputRecipes.forEach(disabled => { table.removeItem(disabled) })
    })
})

EntityEvents.drops(event => {
    event.drops.removeIf(drop => 
    {
        // disable the additional output loots (eg: wither skeletons dropping their stone swords)
        if (global.disabledOutputRecipes.includes(drop.getItem().getId()))
            return true;

        // remove clutter from the wither skeletons by removing their stone swords
        if (event.entity.getType() == 'minecraft:wither_skeleton' && drop.getItem().getId() == 'minecraft:stone_sword')
            return true;

        return false;
    });
});