using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Src.UI.PlayerHUD.Spell.Slot.View {
    public class SpellSlotView : SerializedMonoBehaviour , ISpellSlotView {

        [SerializeField]
        [LabelText("スペルスプライト")]
        private Sprite m_spellSprite;
        
        [SerializeField]
        [LabelText("回数表示")]
        private TMP_Text m_amountView;
        
        public void Dispose() {
            
        }

        public void RemoveSpriteView() {
            throw new System.NotImplementedException();
        }

        public void SpriteViewChange(Sprite sprite) {
            
        }

        public void ValueViewChange(int nextValue) {
            throw new System.NotImplementedException();
        }
        
    }
}