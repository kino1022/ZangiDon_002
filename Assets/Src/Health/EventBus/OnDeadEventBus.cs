using System;
using UnityEngine;

namespace Src.Health.EventBus {

    public interface IOnDeadEventBus {
        
        GameObject Object { get; }
        
    }
    
    public readonly struct OnDeadEventBus : IOnDeadEventBus {
        
        public GameObject Object { get; init; }
        
        public OnDeadEventBus(GameObject obj) {
            
            Object = obj ?? throw new ArgumentNullException();
            
        }
    }
}