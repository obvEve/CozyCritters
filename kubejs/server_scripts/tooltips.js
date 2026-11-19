ItemEvents.modifyTooltips(event => {
    global.disabledOutputRecipes.forEach(recipe => { event.add(recipe, Text.red("This item is disabled!")) })

    global.foundVanillaBackportBatch.forEach(backportItem => {
        event.add(Item.of(backportItem), Text.blue("Vanilla Backport").italic())
    })
})