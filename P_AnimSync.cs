// Copyright © 2022 Pokeyi - https://pokeyi.dev - pokeyi@pm.me - This work is licensed under the MIT License.

using System;
using UdonSharp;
using UnityEngine;
// using UnityEngine.UI;
// using VRC.SDKBase;
// using VRC.SDK3.Components;
// using VRC.Udon.Common.Interfaces;

namespace Pokeyi.UdonSharp
{
    [AddComponentMenu("Pokeyi.VRChat/P.VRC Animation Sync")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)] // No networking.
    [RequireComponent(typeof(Animator))] // Make sure object has an animator component.

    public class P_AnimSync : UdonSharpBehaviour
    {   // UTC animation sync for VRChat:
        [Header(":: VRC Animation Sync by Pokeyi ::")]

        [Header("Keyframe Event: \"_EventSyncUTC\"")]
        [Space] // Animation event keyframe: SendCustomEvent(String)
        [Tooltip("Sync animation to UTC once at start.")]
        [SerializeField] private bool startSyncUTC;
        [Tooltip("Sync animation to UTC when game object is enabled.")]
        [SerializeField] private bool onEnableSyncUTC;
        [Tooltip("Sync animation via keyframe events.")]
        [SerializeField] private bool eventSyncUTC;
        [Tooltip("Sync animation to UTC every frame. (Probably don't.)")]
        [SerializeField] private bool frameSyncUTC;

        private Animator targetAnimator; // Reference to animator component.
        private bool hasStarted = false;

        public void Start()
        {   // Assign reference to animator component and sync animation at start if enabled:
            targetAnimator = GetComponent<Animator>();
            if (startSyncUTC) SyncAnimUTC();
            hasStarted = true;
        }

        public void OnEnable()
        {
            if (!hasStarted) return;
            if (onEnableSyncUTC) SyncAnimUTC();
        }

        public void Update()
        {   // Sync animation every frame if enabled:
            if (frameSyncUTC) SyncAnimUTC();
        }

        private void SyncAnimUTC()
        {   // Retrieve current animation clip length and animator state, sync animation to UTC time:
            if (targetAnimator == null) return;
            AnimatorClipInfo[] currentAnims = targetAnimator.GetCurrentAnimatorClipInfo(0);
            if (currentAnims == null) return;
            AnimationClip currentClip = currentAnims[0].clip;
            if (currentClip == null) return;
            float animationSeconds = currentClip.length;
            int stateHash = targetAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            long utcTicks = DateTime.UtcNow.Ticks; // Convert animation time and decimal spaces to align seconds with UTC ticks:
            float animSingle = Convert.ToSingle(animationSeconds) * 1E+07F;
            long animLong = Convert.ToInt64(animSingle);
            targetAnimator.Play(stateHash, 0, (Convert.ToSingle((utcTicks - ((utcTicks / animLong) * animLong))) / animSingle));
        }

        public void _EventSyncUTC() // *Public/Protected*
        {   // Sync animation via keyframe event if enabled:
            if (eventSyncUTC) SyncAnimUTC();
        }
    }
}

/* MIT License

Copyright (c) 2022 Pokeyi - https://pokeyi.dev - pokeyi@pm.me

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
SOFTWARE. */