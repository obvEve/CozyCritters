EntityJSEvents.modifyEntity(event => {
    event.modify('minecraft:zombie', modifier => {
        modifier.onAddedToWorld(worldAdded => {
            // check for baby
            // then adjust speed to default zombie speed divided by 1.5 since baby zombies will gain +50%
            if (worldAdded.baby)
                worldAdded.getAttribute('minecraft:generic.movement_speed').setBaseValue(0.23 / 1.5)
        })
    })
})