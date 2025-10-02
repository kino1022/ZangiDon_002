using UnityEngine;

namespace Src.Spell.Data.Interface {
    
    public interface ISpellData {
        
        Sprite Sprite { get; }
        
        int MaxAmount { get; }
        
    }
}