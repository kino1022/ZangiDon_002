using System;
using R3;
using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Interface;

namespace Src.Spell.Slot {
    [Serializable]
    public class ASpellSlot<Instance> : ISpellSlot<Instance> where Instance : ISpellInstance {
        
        protected ReactiveProperty<Instance> m_instance;
        
        private bool m_isEmpty = false;
        
        public ReadOnlyReactiveProperty<Instance> Spell => m_instance;

        public bool IsEmpty => m_isEmpty;

        protected ASpellSlot() {
            m_instance = new ReactiveProperty<Instance>();
            RegisterEmptyObserve();
        }

        public void Set(Instance instance) {
            
        }

        public void Remove() {
            
        }

        private void RegisterEmptyObserve() {
            m_instance
                .Subscribe(x => {
                    if (x is null) {
                        m_isEmpty = true;
                    }

                    m_isEmpty = false;
                });
        }
    }
}