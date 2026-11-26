const $MobEffectCategory = Java.loadClass("net.minecraft.world.effect.MobEffectCategory")
const $SleepFinishedTimeEvent = Java.loadClass("net.neoforged.neoforge.event.level.SleepFinishedTimeEvent")

NativeEvents.onEvent($SleepFinishedTimeEvent, event => {
    event.level.players.forEach(player => {
        if (player.isSleeping()) {
            onPlayerSlept(player)
        }
    })
});

function onPlayerSlept(player) {
    player.heal(5)

    player.activeEffects.toArray().forEach(effect => {
        const category = effect.effect.value().getCategory()

        if (category == $MobEffectCategory.HARMFUL) {
            player.removeEffect(effect.effect)
        }
    })
}