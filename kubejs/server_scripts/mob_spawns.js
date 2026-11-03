/*

EntityEvents.spawned(event => {
    if (event.entity.type == "minecraft:skeleton") {
        let baseHealth = event.entity.maxHealth
        let mobHealthChange = -6
        let newMaxHealth = baseHealth + mobHealthChange
        event.entity.modifyAttribute("minecraft:generic.max_health", "health.rebalances", newMaxHealth - event.entity.maxHealth, "add_value")
    }
})

*/