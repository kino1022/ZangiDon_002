using UnityEngine;

namespace Src.Health {

    public interface IDamage {
        int Value { get; }
    }
        
    public struct Damage : IDamage {
        
        public int Value { get; init; }
        
        public Damage(int value) => Value = value;
        
    }
    
}