using System;
using Src.Spell.EventBus.Interface;
using Src.Spell.Instance.Cloner;
using Src.Spell.Instance.Interface;
using Src.Spell.Instance.Main.Interface;
using Src.Spell.Instance.Sub.Interface;

namespace Src.Spell.EventBus {
    public readonly struct OnSelectEventBus : IOnSelectEventBus {

        private readonly ISpellInstance m_spell;
        
        public ISpellInstance Spell => m_spell;

        public OnSelectEventBus(ISpellInstance spell) {
            
            if (spell is null) throw new ArgumentNullException(nameof(spell));

            //参照切りするためのクローン処理
            if (spell is IMainSpellInstance main) {
                m_spell = new MainSpellCloner().Clone(main) ?? throw new NullReferenceException();
                return;
            }

            //サブの場合のクローン処理
            if (spell is ISubSpellInstance sub) {
                m_spell = new SubSpellCloner().Clone(sub) ?? throw new NullReferenceException();
                return;
            }
            
            m_spell = spell;
        }
        
    }
}