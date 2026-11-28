KeyBindJSEvents.modify(event => {
    // remove iris keybinds that can be accidentally clicked and cause reloads
    event.remove('iris.keybind.reload')
    event.remove('iris.keybind.toggleShaders')

    // hide the inventory sort key and set its default
    event.modifyKey('key.inventorytweak.sort_inventory_key', GLFW.GLFW_MOUSE_BUTTON_MIDDLE)
    event.addHideKey('key.inventorytweak.sort_inventory_key')

    // default options of hand to R
    // should be used for doing things with currently held item
    event.modifyKey('create.keyinfo.rotate_menu', GLFW.GLFW_KEY_R)
    event.modifyKey('key.constructionstick.open_gui', GLFW.GLFW_KEY_R)

    // other default keybinds
    event.modifyKey('key.voice_chat', GLFW.GLFW_KEY_P)

    // hide keybinds that are useful but aren't necessary to change
    event.addHideKey('key.ponder.ponder')

    // remove keybinds that go against the immersion
    event.addHideKey('key.ftbquests.quest') // hidden for user experience, however it is defaulted to no bind
    event.remove('key.fieldguide.open')
    event.remove('key.fieldguide.scan')

    // remove keybinds that aren't necessary for playing
    event.remove('key.modernfix.config')
    event.remove('placebo.toggleTrails')
    event.remove('placebo.toggleWings')
    event.remove('key.ftbfiltersystem.get_or_filter_hotbar')
    event.remove('key.ftbfiltersystem.get_or_filter_inv')
    event.remove('key.kubejs.kubedex')
    event.remove('iris.keybind.wireframe')
    event.remove('framedblocks.key.update_cull')
    event.remove('framedblocks.key.wipe_cache')
    event.remove('key.socialInteractions')
})