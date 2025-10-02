using Sirenix.OdinInspector;
using Src.Spell.Data.Interface;
using UnityEngine;

namespace Src.Spell.Data {
    public abstract class ASpellData : SerializedScriptableObject , ISpellData {

        [SerializeField]
        private Sprite m_sprite;

        [SerializeField, ProgressBar(0,20)]
        private int m_amount = 0;
        
        public Sprite Sprite => m_sprite;

        public int MaxAmount => m_amount;
    }
}