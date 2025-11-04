using UnityEngine;

namespace Src.Health {
    
    public interface IHeal {
        
        int Value { get; }
        
    }
    
    public struct Heal : IHeal {
        
        public int Value { get; init; }
        
        public Heal (int value) => Value = value;
        
    }
}