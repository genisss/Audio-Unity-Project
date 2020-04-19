# Unreal Engine Introduction Project
[![](https://img.youtube.com/vi/MzaYSEiA7uI/maxresdefault.jpg)](https://youtu.be/MzaYSEiA7uI)


## Introduction and controls
The aim of this minigame is to kill as much gophers as possible within the time and avoiding kicking the cute bunnies.

 - **WASD** to Move
 - **R** to Reload
 - **1,2,3** to change the weapon
 - **Left Mouse** click to fire

## Implementations

**AudioMixers**

 - **SFXMixer**
	 - *Music:* Contains de ambient music for both inside and outisde spaces and implements a sidechain compressor
	 - *Player Group:* Contains the steps and weapons sounds and it's the input for the Music group sidechain compressor
	 - *Enemies:* Contains de sounds for the enemies when hit


- **UI:**  Contains 2d UI sounds

**AudioSources**
 - **AudioManager:** It has 2 Audio Sources 2D that ara used for the background Music. The first one is used for the *inside* and the *end game* music and the second is used for the *outside* music. This implementations allows us tho do a soft transition between bg musics.

 - **GameUI** it has another 2D audio source made for the UI sounds

- **Guns** each gun has an audiosource in 2D that plays the fire and reload sounds
 - **Force shield material** used in the invisible obstacles
 - **Footsteps**: it has a 2d Audiosource with randomizes the player footsteps, jump and landing sounds.
 - **Door:** a 3d AudioSource for the opening and closing door SFX
 - **Pooled AudioSources**: we have two pools, one for the impacts of the projectiles and another for the sounds of the gophers and bunny
