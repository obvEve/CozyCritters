/* 
 * ServerEvents.recipes(callback) is a function that accepts another function,
 * called the "callback", as a parameter. The callback gets run when the 
 * server is working on recipes, and then we can make our own changes.
 * When the callback runs, it is also known as the event "firing". 
 * Refer to https://kubejs.com/wiki/tutorials/recipes
*/

// Listen for the "recipes" server event.
ServerEvents.recipes(event => {
    global.disabledOutputRecipes.forEach(recipe => { event.remove({ output: recipe }) })
})