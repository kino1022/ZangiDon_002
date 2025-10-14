using Src.Spell.CastAction.Interface;
using Src.Spell.Instance.Interface;
using UnityEngine;

namespace Src.Spell.Instance.Main.Interface {
    /// <summary>
    /// メインスペルのインスタンスに対して約束するインターフェース
    /// </summary>
    public interface IMainSpellInstance : ISpellInstance {
        
        ICastAction OnCast { get; init; }
        
    }
    
}