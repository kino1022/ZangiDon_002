using System;
using UnityEngine;

namespace Src.Health.EventBus {

    public interface ITakeDamageEventBus {
        
        GameObject Object { get; }
        
        IDamage Damage { get; }
    }
    
    public readonly struct TakeDamageEventBus : ITakeDamageEventBus {
        
        public GameObject Object { get; init; }
        
        public IDamage Damage { get; init; }

        public TakeDamageEventBus(GameObject obj, IDamage damage) {
            
            Object = obj ?? throw new ArgumentNullException();
            
            Damage = damage ?? throw new ArgumentNullException();
            
        }
    }
}