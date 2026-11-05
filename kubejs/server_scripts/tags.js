ServerEvents.tags('entity_type', event => {
    event.add(global.packIdentifier + ':overhauled_enderman', /^endermanoverhaul:.*enderman$/);
});

ServerEvents.tags('item', event => {
    event.add(global.packIdentifier + ':macawbridges', '@mcwbridges');
    event.add(global.packIdentifier + ':macawdoors', '@mcwdoors');
    event.add(global.packIdentifier + ':macawfences', '@mcwfences');
    event.add(global.packIdentifier + ':macawfurnitures', '@mcwfurnitures');
    event.add(global.packIdentifier + ':macawlights', '@mcwlights');
    event.add(global.packIdentifier + ':macawpaths', '@mcwpaths');
    event.add(global.packIdentifier + ':macawstairs', '@mcwstairs');
    event.add(global.packIdentifier + ':macawtrapdoors', '@mcwtrpdoors');
    event.add(global.packIdentifier + ':macawwindows', '@mcwwindows');
})