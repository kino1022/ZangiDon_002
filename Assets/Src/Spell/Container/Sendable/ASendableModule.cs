using System;
using GeneralModule.Status;
using Src.Spell.Container.Sendable.Interface;
using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Slot.Interface;

namespace Src.Spell.Container.Sendable {
    public abstract class ASendableModule<Manager,Instance,Slot> : ISpellSendableManager<Manager,Instance,Slot> 
        where Manager : ISpellManager<Instance,Slot>
        where Instance : ISpellInstance
        where Slot : ISpellSlot<Instance>
    {
        protected Manager m_manager;

        public bool Sendable => CalculateSendable();
        
        protected ASendableModule(Manager manager) {
            m_manager = manager ?? throw new ArgumentNullException();
        }

        protected bool CalculateSendable() {
            for (int i = 0; i < m_manager.Length; i++) {
                var slot = m_manager.Spells[i];
                if (slot.IsEmpty) {
                    return true;
                }
            }

            return false;
        }
    }
}