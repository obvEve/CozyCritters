const $BuiltInRegistries = Java.loadClass('net.minecraft.core.registries.BuiltInRegistries')

const entityToPlushOverrides = {
    'minecraft:trader_llama': 'plushie_buddies:plushie_trader_lama',
    'minecraft:ravager': 'plushie_buddies:plushie_ravanger',
    'minecraft:ender_dragon': 'plushie_buddies:plushie_enderdragon'
}

function getAllPlushies() {
    return Ingredient.of('@plushie_buddies').itemIds.toArray().filter(item => item.split(':')[1].contains('plushie_'))
}

function getPlushEntity(plush) {
    for (const [entityId, plushieId] of Object.entries(entityToPlushOverrides))
    {
        if (plushieId == plush)
            return entityId;
    }

    const fullPlushieName = plush.split(':')[1]
    const plushName = fullPlushieName.split('plushie_')[1]

    for (const entity of $BuiltInRegistries.ENTITY_TYPE) {
        if (entity.idLocation.path == plushName) {
            return entity.idLocation;
        }
    }
}

ServerEvents.loaded(event => {
    const plushies = getAllPlushies()

    for (const plush of plushies) {
        // const does not work here
        let plushEntity = getPlushEntity(plush)

        if (plushEntity) {
            entityToPlushOverrides[plushEntity] = plush
        } else {
            console.log(`No matching entity found for: ${plush}`)
        }
    }

    for (const entity of $BuiltInRegistries.ENTITY_TYPE) {
        let associatedPlush = null;
        if (entity.hasTag('c:creepers')) { // check creepers
            associatedPlush = 'plushie_buddies:plushie_creeper'
        }

        else if (entity.hasTag(global.packIdentifier + ':overhauled_enderman')) { // check endermen
            associatedPlush = 'plushie_buddies:plushie_enderman'
        }
        else if (entity.hasTag('spider_overhaul:spiders')) { // check spiders
            associatedPlush = 'plushie_buddies:plushie_spider'
        }

        if (associatedPlush)
            entityToPlushOverrides[entity.idLocation] = associatedPlush;
    }
})


FieldGuideEvents.entryUnlocked(event => {
    if (!event.newUnlock)
        return

    const plush = entityToPlushOverrides[event.rawEntryId]
    if (!plush)
        return


    event.player.give(plush)
})
/*
if (event.player.persistentData.getBoolean(getPersistentPath(plush)))
   return

event.player.persistentData.putBoolean(getPersistentPath(plush), true)

function getPersistentPath(plushId) {
    return `hasAcquired_${plushId}`
}
*/