# Animation Module
this module lets you play an animation on items
the main idea I had with it is letting the user make
a json with animation data which it later uses.

AnimationJsons folder needs to be place in `BladeAndSorcery_Data/StreamingAssets/Mods`.

The dll needs to be inside the mod's folder.

the animation data Json is structured like so

```json
{
	"openAnimationName": "Extend",
	"closeAnimationName": "Retract",
	"currentlyClosed": true
}
```
and inside the item json under modules you put

```json
{
	"$type": "AnimationModule.AnimationData, AnimationModule",
  	"bindToButton": "spellMenu",
  	"animationComponent": "animation component refrence",
  	"animationHandles": {
	  "HandleName": "AnimationDataJson"
  	}
}
``` 
