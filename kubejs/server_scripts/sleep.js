const $PlayerWakeUpEvent = Java.loadClass("net.neoforged.neoforge.event.entity.player.PlayerWakeUpEvent")
const $MobEffectCategory = Java.loadClass("net.minecraft.world.effect.MobEffectCategory")

// FIX THIS GIVING IT FOR EXITING BED, NOT FULLY SLEEPING
NativeEvents.onEvent($PlayerWakeUpEvent, event => {
    const player = event.entity;

    player.heal(5)

    player.activeEffects.toArray().forEach(effect => {
        const category = effect.effect.value().getCategory()

        if (category == $MobEffectCategory.HARMFUL) {
            player.removeEffect(effect.effect)
        }
    })
})

/*
const $SleepFinishedTimeEvent = Java.loadClass(
    "net.neoforged.neoforge.event.level.SleepFinishedTimeEvent"
)

loop players, if sleeping do the above logic

*/