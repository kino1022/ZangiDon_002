using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Data.Sub.Interface;
using Src.Spell.CastAction.Interface;
using UnityEngine;

namespace Src.Spell.Data.Sub {
    [CreateAssetMenu(menuName = "Project/SpellData/Sub")]
    public class SubSpellData : ASpellData, ISubSpellData {
        
        [TitleGroup("性能")]
        [OdinSerialize]
        [LabelText("選択時の効果")]
        private IOnSelectAction m_selectAction;

        [TitleGroup("性能")]
        [OdinSerialize] [LabelText("使用直前の効果")] 
        private IPreCastAction m_preCast;
        
        [TitleGroup("性能")]
        [OdinSerialize]
        [LabelText("使用直後の効果")]
        private IPostCastAction m_postCast;
        
        public IOnSelectAction OnSelect => m_selectAction;
        public IPreCastAction PreCast => m_preCast;
        public IPostCastAction PostCast => m_postCast;
    }
}