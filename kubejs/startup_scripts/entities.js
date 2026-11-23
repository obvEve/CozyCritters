EntityJSEvents.modifyEntity(event => {
    event.modify('minecraft:zombie', modifier => {
        modifier.onAddedToWorld(worldAdded => {
            // check for baby
            // then adjust speed
            if (worldAdded.baby)
                worldAdded.getAttribute('minecraft:generic.movement_speed').setBaseValue(0.2)

            /*
            player.getAttribute('neoforge:swim_speed').addOrReplacePermanentModifier({
    operation: 'add_value',
    id: 'kubejs:extra_swim_speed',
    amount: 1,
            }) 
            */
        })
    })
})