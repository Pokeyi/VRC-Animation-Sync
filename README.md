# [💾](https://github.com/Pokeyi/VRC-Animation-Sync/blob/main/P_AnimSync.cs) VRC-Animation-Sync [![Downloads](https://img.shields.io/github/downloads/Pokeyi/VRC-Animation-Sync/total?label=Downloads&logo=github)](https://github.com/Pokeyi/VRC-Animation-Sync/releases)
 UTC animation sync for VRChat. 
 
![Anim Sync Image](P_AnimSync.png)

### VRChat Creator Companion App (VCC)
***This project can now be imported through VCC with the [Pokeyi's Udon Tools](https://github.com/Pokeyi/vpm-packages#vpm-packages) VPM package.***

## 🛑 Important Note 🛑
This script is intended as an easy syncing solution for *simple looping animations* without important start/stop sequences or transitions. Please read the documentation for details on its use including [UTC-Sync](https://github.com/Pokeyi/VRC-Animation-Sync#utc-sync) and [Use Case Examples](https://github.com/Pokeyi/VRC-Animation-Sync#use-case-examples) for possible caveats.

You can not blindly slap this script on anything with an animation and expect it to sync properly.

## Overview
VRC Animation Sync is a single configurable UdonSharp behaviour that can be used to easily sync animations for all players without networking.

It is intended to be efficient and relatively simple to use without the need for any additional editor scripts or dependencies outside of UdonSharp. All configuration can be done within the Unity Inspector window without the need for any programming, Udon, or SDK knowledge. That said, the source code is cleanly-organized and commented in the hopes of also being a good learning tool.

### Requirements
Errors regarding functions not being exposed likely mean you need an updated version of the SDK or UdonSharp.
- [Unity](https://docs.vrchat.com/docs/current-unity-version) (Tested: v2019.4.31f1)
- [VRChat Worlds SDK3](https://vrchat.com/home/download) (Tested: v2021.11.8)
- [UdonSharp](https://github.com/MerlinVR/UdonSharp) (Tested: v0.20.3)

### Recommended
Other useful VRChat world-creation tools that I will always recommend.
- [World Creator Assistant](https://github.com/Varneon/WorldCreatorAssistant) (SDK & Package Management)
- [CyanEmu](https://github.com/CyanLaser/CyanEmu) (Unity-Window Testing)
- [VRWorld Toolkit](https://github.com/oneVR/VRWorldToolkit) (World Debugger)

### Setup
Make sure you have already imported the VRChat Worlds SDK and UdonSharp into your project.
- Download the latest [Unity Package](https://github.com/Pokeyi/VRC-Animation-Sync/releases) and import it into your project.
- A pre-configured example prefab is included that you can drop into your scene if you like.
- Create and select an empty game object and add the P_AnimSync behaviour via the Unity Inspector window or 'Component > Pokeyi.VRChat > P.VRC Animation Sync' toolbar menu.
- Click the 'Convert to UdonBehaviour' button if prompted.
- Configure the rest of the behaviour's properties in the Inspector window as you see fit. Each of these is explained in detail further below.

## Features
The main features of Animation Sync.
- Sync Toggles - Designate at which points in runtime you want the animation to synchronize.
- Keyframe Event - Method for triggering re-synchronization at certain keyframes sent from the animation.
- UTC-Sync - The method used for syncing animations to all players.

All of the following properties have hover-tooltips in the Unity Inspector window.

### Sync Toggles
Designate at which points in runtime you want the animation to synchronize.
- Start Sync UTC - Sync animation once at start.
- On Enable Sync UTC - Sync animation each time game object is enabled.
- Event Sync UTC - Sync animation via called events.
- Frame Sync UTC - Sync animation every frame.

### Keyframe Event
Call the public "\_EventSyncUTC" method to trigger a re-synchronization. (See: Notes [#1](#notes))
- This can be done without any programming by selecting 'SendCustomEvent(String)' from the dropdown when adding a Keyframe Event to a keyframe in your animation.

### UTC-Sync
UTC-Sync is a method of syncing motions or events for all players without the use of networking by aligning them with Universal Time as a shared frame of reference.
- The main caveat to this form of syncing is that each player's local machine must have had its clock [synchronized](https://youtu.be/VZBxG6v0gYQ). This is usually done automatically by Windows. The latest synchronization can be checked in your Date & Time Settings, but it should be very rare that this would need to be worried about manually.
- Since they are synced to Universal Time and not a player or network event, animations will behave as if they have always been playing even before the world loaded and will be synced to the same playback position regardless of instance or player join times, so it is best not to use this method to sync animations with important start/stop times or transitions.

### Use Case Examples
For now, this behaviour is best suited for simple looping animations. It should support changing animations as long as you re-sync afterwards, but the transition may not be smooth.
- In most use cases, for a looping animation that starts on Awake, you would only need to sync once at start.
- I also prefer to put a Keyframe Event at the last frame of each animation to resync, just in case.
- 'Frame Sync UTC' should not be necessary for most use cases and is probably not great for performance either.

### Notes
1. Per the VRChat API, public method/event names starting with an "\_Underscore" are protected from remote network calls, necessitating use of a local-only event. Doing this protects them from being called by malicious clients and potentially breaking functionality in your world.

## Credit & Support
Please credit me as Pokeyi if you use my work. I would also love to see your creations that make use of it if you're inclined to share. This and [related projects](https://github.com/Pokeyi/pokeyi.github.io#my-projects) have involved many months of solid work and self-education as I strive for an opportunity to change careers and make a better life for myself. If you find value in my work, please consider supporting me, it would truly help a lot.

[![Patreon](https://img.shields.io/badge/Patreon-Support-red?logo=patreon)](https://patreon.com/pokeyi)

[![PayPal](https://img.shields.io/badge/PayPal-Donate-blue?logo=paypal)](https://www.paypal.com/donate?hosted_button_id=XFBLJ5GNSLGRC)

## License
This work is licensed under the MIT License.

Copyright © 2022 Pokeyi - https://pokeyi.dev - [pokeyi@pm.me](mailto:pokeyi@pm.me)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
