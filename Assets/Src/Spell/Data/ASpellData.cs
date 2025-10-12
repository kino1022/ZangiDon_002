using Sirenix.OdinInspector;
using Src.Spell.Data.Interface;
using UnityEngine;

namespace Src.Spell.Data {
    public abstract class ASpellData : SerializedScriptableObject , ISpellData {

        [TitleGroup("UI要素")]
        [SerializeField]
        private Sprite m_sprite;

        [TitleGroup("性能")]
        [SerializeField, ProgressBar(0,20)]
        private int m_amount = 0;
        
        public Sprite Sprite => m_sprite;

        public int MaxAmount => m_amount;
    }
}