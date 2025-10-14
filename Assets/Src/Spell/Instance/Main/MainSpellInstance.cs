using System;
using GeneralModule.Amount.Interface;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.CastAction.Interface;
using Src.Spell.Instance.Main.Interface;
using UnityEngine;

namespace Src.Spell.Instance.Main
{
    public record MainSpellInstance : ASpellInstance, IMainSpellInstance
    {
        [OdinSerialize]
        [LabelText("使用時の効果")]
        protected ICastAction m_onCast;

        public ICastAction OnCast {
            get { return m_onCast; }
            init {
                m_onCast = value;
            }
        }

        public MainSpellInstance(
            ICastAction castAction,
            Sprite sprite,
            IAmountModule amountModule
        ) :
        base(sprite, amountModule)
        {
            OnCast = castAction ?? throw new ArgumentNullException(nameof(castAction));
        }
    }
}