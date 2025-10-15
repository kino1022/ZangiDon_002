using System;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Interface;
using UnityEngine;

namespace Src.Spell.Slot {
    [Serializable]
    public class ASpellSlot<Instance> : ISpellSlot<Instance> where Instance : ISpellInstance {
        
        [OdinSerialize]
        protected ReactiveProperty<Instance> m_instance;
        
        [SerializeField]
        [ReadOnly]
        private bool m_isEmpty = true;
        
        [OdinSerialize]
        public ReadOnlyReactiveProperty<Instance> Spell => m_instance;

        [ShowInInspector]
        private Instance SetInstance => Spell.CurrentValue;

        public bool IsEmpty => m_isEmpty;

        protected ASpellSlot() {
            m_instance = new ReactiveProperty<Instance>();
            RegisterEmptyObserve();
        }

        public void Set(Instance instance) {
            if (instance is null) {
                throw new ArgumentNullException(nameof(instance));
            } 
            m_instance.Value = instance;
            m_isEmpty = false;
        }

        public void Remove() {
            m_instance.Value = default;
            m_isEmpty = true;
        }

        private void RegisterEmptyObserve() {
            /*
            m_instance
                .Subscribe(x => {
                    if (x.Equals(default)) {
                        m_isEmpty = true;
                    }
                    m_isEmpty = false;
                });
                */
            
            //instanceのequalを読んでいる関係でこのラムダ式が呼ばれるとクラッシュする
            //対抗策はのちのち考えるので用修正
        }
    }
}