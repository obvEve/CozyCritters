// ItemEvents.modifyTooltips
ItemEvents.modifyTooltips(event => {
    global.disabledOutputRecipes.forEach(recipe => { event.add(recipe, Text.red("This item is disabled!")) })
})