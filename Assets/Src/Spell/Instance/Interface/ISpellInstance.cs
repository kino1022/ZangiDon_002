using GeneralModule.Amount.Interface;
using UnityEngine;

namespace Src.Spell.Instance.Interface {
    /// <summary>
    /// 
    /// </summary>
    public interface ISpellInstance {
        
        /// <summary>
        /// スペルの画面表示用スプライト
        /// </summary>
        Sprite Sprite { get; init; }
        
        /// <summary>
        /// 残りの使用回数
        /// </summary>
        IAmountModule Amount { get; init; }
        
    }
}