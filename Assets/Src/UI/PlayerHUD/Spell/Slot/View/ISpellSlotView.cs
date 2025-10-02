using System;
using UnityEngine;

namespace Src.UI.PlayerHUD.Spell.Slot.View {
    public interface ISpellSlotView : IDisposable {
        
        /// <summary>
        /// 表示するスプライトを変化させる
        /// </summary>
        /// <param name="sprite"></param>
        void SpriteViewChange(Sprite sprite);
        
        /// <summary>
        /// 表示されているスプライトを無効化する
        /// </summary>
        void RemoveSpriteView();
        
        /// <summary>
        /// 表示する値を変化させる
        /// </summary>
        /// <param name="nextValue"></param>
        void ValueViewChange(int nextValue);
    }
}