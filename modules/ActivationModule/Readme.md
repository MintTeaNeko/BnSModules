# Activation Module U11

AnimationJsons folder needs to be place in `BladeAndSorcery_Data/StreamingAssets/Mods`.

The dll needs to be inside the mod's folder `BladeAndSorcery_Data/StreamingAssets/Mods/{Your mod's name}`.

and inside the item json under modules you put

```json
{
  "$type": "ActivationModule.Module, ActivationModule",
  "ButtonBind": "trigger",
  "StartsActivated": false,
  "AnimationRefrence": "",
  "ActivationAnimName": "",
  "DeActivationAnimName": "",
  "ActivationSounds": [
    "soundName"
  ],
  "DeactivationSounds": [
    "soundName"  
  ]
}
``` 
