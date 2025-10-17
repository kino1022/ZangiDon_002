using System;
using UnityEngine;

namespace Src.Health.EventBus {

    public interface ITakeDamageEventBus {
        
        GameObject Object { get; }
        
        int Damage { get; }
    }
    
    public readonly struct TakeDamageEventBus : ITakeDamageEventBus {
        
        public GameObject Object { get; init; }
        
        public int Damage { get; init; }

        public TakeDamageEventBus(GameObject obj, int damage) {
            
            Object = obj ?? throw new ArgumentNullException();
            
            Damage = damage;
            
        }
    }
}