KeyBindJSEvents.modify(event => {
    // remove iris keybinds that can be accidentally clicked and cause reloads
    event.remove('iris.keybind.reload')
    event.remove('iris.keybind.toggleShaders')

    // hide the inventory sort key and set its default
    event.addHideKey('key.inventorytweak.sort_inventory_key')
    event.modifyKey('key.inventorytweak.sort_inventory_key', GLFW.GLFW_MOUSE_BUTTON_MIDDLE)

    // default options of hand to R
    event.modifyKey('create.keyinfo.rotate_menu', GLFW.GLFW_KEY_R)

    // hide other keybinds that aren't really necessary to change
    event.addHideKey('key.ponder.ponder')

    // remove keybinds that aren't necessary for playing
    event.remove('key.modernfix.config')
})