LootJS.lootTables(event => {
    event.forEachTable(table => {
        global.disabledOutputRecipes.forEach(disabled => { table.removeItem(disabled) })
    })
})