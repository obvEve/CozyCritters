/* 
 * ServerEvents.recipes(callback) is a function that accepts another function,
 * called the "callback", as a parameter. The callback gets run when the 
 * server is working on recipes, and then we can make our own changes.
 * When the callback runs, it is also known as the event "firing". 
 * Refer to https://kubejs.com/wiki/tutorials/recipes
*/

// [output, dye, ingredient]
const bunkDying = [
    ['biomesoplenty:black_sand', '#c:dyes/black', '#minecraft:sand'],
    ['biomesoplenty:orange_sand', '#c:dyes/orange', '#minecraft:sand'],
    ['biomesoplenty:white_sand', '#c:dyes/white', '#minecraft:sand'],
    ['minecraft:red_sand', '#c:dyes/red', '#minecraft:sand'],
]

// [soil input, soil output]
const soilToFarmland = [
    ['farmersdelight:rich_soil', 'farmersdelight:rich_soil_farmland']
]

function potInteractionRecipe(/** @type {$RecipesKubeEvent} */ event, heldItem, soilItem, newSoil) {
    event.custom({
        type: 'botanypots:pot_interaction',
        held_item: heldItem,
        soil_item: soilItem,
        new_soil: {
            id: newSoil
        },
        sound_effect: {
            id: 'minecraft:item.hoe.till',
            category: 'BLOCKS'
        }
    })
}

// Listen for the "recipes" server event.
ServerEvents.recipes(event => {
    // Disable certain recipes
    global.disabledOutputRecipes.forEach(recipe => { event.remove({ output: recipe }) })

    // Create
    event.recipes.create.mixing('minecraft:cobblestone', [Fluid.of('minecraft:water', 100), Fluid.of('minecraft:lava', 100)]);
    event.recipes.create.mixing('minecraft:stone', [Fluid.of('minecraft:water', 100), Fluid.of('minecraft:lava', 100)]).heated();
    event.recipes.create.mixing('minecraft:obsidian', [Fluid.of('minecraft:water', 250), Fluid.of('minecraft:lava', 250)]).heated();
    event.recipes.create.mixing('minecraft:basalt', [Fluid.of('minecraft:lava', 100), 'minecraft:blue_ice', 'minecraft:soul_soil']);
    event.recipes.create.mixing('create:scoria', [Fluid.of('create:chocolate', 100), Fluid.of('minecraft:lava', 100)]);
    event.recipes.create.mixing('create:limestone', [Fluid.of('create:honey', 100), Fluid.of('minecraft:lava', 100)]);

    // Create - Bulk Dying
    bunkDying.forEach(([output, dye, ingredient]) => {
        event.shaped(Item.of(output, 8), 
        [
            'AAA',
            'ABA',
            'AAA'
        ],
        {
            A: ingredient,
            B: dye
        })
    });

    // Botany pot interactions
    // source: https://github.com/Darkhax-Minecraft/BotanyPots/blob/1.21.1/common/src/main/resources/data/botanypots/recipe/minecraft/interaction/till_farmland.json
    soilToFarmland.forEach(([soilInput, soilOutput]) => {
        potInteractionRecipe(
            event,
            { tag: 'minecraft:hoes' },
            { item: soilInput },
            soilOutput
        )
    })
})