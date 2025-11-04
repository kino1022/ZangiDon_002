using System;
using UnityEngine;

namespace Src.Sound {
    
    public interface IEmitSoundEventBus {
        
        AudioClip Clip { get; }
        
    }
    
    public readonly struct EmitSoundEventBus : IEmitSoundEventBus {
        
        public AudioClip Clip { get; }
        
        public EmitSoundEventBus(AudioClip clip) {
            Clip = clip ?? throw new ArgumentNullException();
        }
        
    }
}