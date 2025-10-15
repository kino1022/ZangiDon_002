using System;
using GeneralModule.Amount.Interface;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.CastAction.Interface;
using Src.Spell.Instance.Sub.Interface;
using UnityEngine;

namespace Src.Spell.Instance.Sub {
    [Serializable]
    public record SubSpellInstance :ASpellInstance, ISubSpellInstance  {
        
        [Title("効果")]
        [OdinSerialize]
        [LabelText("選択時の効果")]
        private IOnSelectAction m_onSelect;
        
        [OdinSerialize]
        [LabelText("発動直前の効果")]
        private IPreCastAction m_preCast;
        
        [OdinSerialize]
        [LabelText("発動直後の効果")]
        private IPostCastAction m_postCast;

        public IOnSelectAction OnSelect {
            get => m_onSelect;
            init => m_onSelect = value;
        }

        public IPreCastAction PreCast {
            get => m_preCast;
            init => m_preCast = value;
        }

        public IPostCastAction PostCast {
            get => m_postCast;
            init => m_postCast = value;
        }

        public SubSpellInstance(
            Sprite sprite,
            IAmountModule amountModule,
            IOnSelectAction onSelect,
            IPreCastAction preCast,
            IPostCastAction postCast
        ) : base(sprite, amountModule) {
            OnSelect = onSelect;
            PreCast = preCast;
            PostCast = postCast;
        }
    }
}