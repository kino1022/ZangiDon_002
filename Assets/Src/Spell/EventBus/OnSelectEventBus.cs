using System;
using Src.Spell.EventBus.Interface;
using Src.Spell.Instance.Interface;

namespace Src.Spell.EventBus {
    public readonly struct OnSelectEventBus : IOnSelectEventBus {

        private readonly ISpellInstance m_spell;
        
        public ISpellInstance Spell => m_spell;

        public OnSelectEventBus(ISpellInstance spell) {
            m_spell = spell ?? throw new ArgumentNullException();
        }
    }
}