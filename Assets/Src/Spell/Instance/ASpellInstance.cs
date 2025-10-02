using System;
using GeneralModule.Amount.Interface;
using Sirenix.Serialization;
using Src.Spell.Instance.Interface;
using UnityEngine;

namespace Src.Spell.Instance {
    [Serializable]
    public abstract record ASpellInstance : ISpellInstance {

        [SerializeField]
        protected Sprite m_sprite;
        
        [OdinSerialize]
        protected IAmountModule m_amount;
        
        public Sprite Sprite {
            get {return m_sprite;}
            init {
                m_sprite = value;
            }
        }


        public IAmountModule Amount {
            get {return m_amount;}
            init {
                m_amount = value;
            }
        }
        
        protected ASpellInstance(Sprite sprite, IAmountModule amount) {
            Sprite = sprite;
            Amount = amount;
        }
        
    }
}