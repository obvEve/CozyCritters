ItemEvents.modifyTooltips(event => {
    global.disabledOutputRecipes.forEach(recipe => { event.add(recipe, Text.red("This item is disabled!")) })

    // Vanilla backports
    // we load all stacks from the creative tab
    Ingredient.of("%vanillabackport:vanilla_backport").stacks.forEach(stack => {
        event.add(stack, Text.blue("Vanilla Backport").italic())
    })
})