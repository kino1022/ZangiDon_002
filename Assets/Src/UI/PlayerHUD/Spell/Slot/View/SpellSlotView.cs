using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.UI.PlayerHUD.Spell.Slot.View {
    public class SpellSlotView : SerializedMonoBehaviour , ISpellSlotView {

        [SerializeField]
        [LabelText("スペルスプライト")]
        private Image m_spellSprite;
        
        [SerializeField]
        [LabelText("回数表示")]
        private TMP_Text m_amountView;
        
        public void Dispose() {
            
        }

        public void RemoveSpriteView() {
            m_spellSprite.sprite = null;
            m_spellSprite.gameObject.SetActive(false);
        }

        public void SpriteViewChange(Sprite sprite) {
            m_spellSprite.gameObject.SetActive(true);
            m_spellSprite.sprite = sprite;
        }

        public void ValueViewChange(int nextValue) {
            m_amountView.text = nextValue.ToString();
        }
        
    }
}