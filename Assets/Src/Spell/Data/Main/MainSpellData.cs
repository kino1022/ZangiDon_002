using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Data.Main.Interface;
using Src.Spell.Instance.CastAction.Interface;
using UnityEngine;

namespace Src.Spell.Data.Main {
    [CreateAssetMenu(menuName = "Project/SpellData/Main")]
    public class MainSpellData : ASpellData , IMainSpellData {

        [TitleGroup("性能")] [OdinSerialize] [LabelText("使用時の挙動")]
        private ICastAction m_cast;
        
        public ICastAction OnCast => m_cast;
    }
}