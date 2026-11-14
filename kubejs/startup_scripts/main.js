global.packIdentifier = 'cozycritters';

global.disabledOutputRecipes = ['sophisticatedbackpacks:feeding_upgrade',
    'sophisticatedbackpacks:advanced_feeding_upgrade',
    'sophisticatedstorage:feeding_upgrade',
    'sophisticatedstorage:advanced_feeding_upgrade',
    'artifacts:eternal_steak',
    'artifacts:everlasting_beef',
    'constructionstick:template_battery',
    'sophisticatedbackpacks:battery_upgrade',
    'framedblocks:powered_framing_saw'];

global.disabledWithReplacement = ['croptopia:tomato', 'croptopia:cabbage', 'croptopia:onion'];

global.disabledWithReplacement.forEach(element => {
    if (element.includes('croptopia'))
        global.disabledWithReplacement.push(element + "_seed")
});