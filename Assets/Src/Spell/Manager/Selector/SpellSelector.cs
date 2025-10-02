using System;
using MessagePipe;
using Sirenix.OdinInspector;
using Src.Spell.Container.Sendable.Interface;
using Src.Spell.EventBus;
using Src.Spell.EventBus.Interface;
using Src.Spell.Instance;
using Src.Spell.Instance.Interface;
using Src.Spell.Instance.Main.Interface;
using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Manager.Selector.Interface;
using Src.Spell.Slot.Interface;
using Src.Spell.Slot.Selector.Interface;
using VContainer;

namespace Src.Spell.Manager.Selector {
    public class SpellSelector : ASpellManager<ISpellInstance,ISelectorSlot> , ISpellSelector {

        private IMainSendableManager m_mainSendable;
        
        private ISubSendableManager m_subSendable;
        
        private IPublisher<IOnSelectEventBus> m_publisher;

        public override void Start() {
            
            base.Start();
            
            m_mainSendable = m_resolver.Resolve<IMainSendableManager>();
            m_subSendable = m_resolver.Resolve<ISubSendableManager>();
            m_publisher = m_resolver.Resolve<IPublisher<IOnSelectEventBus>>();
            
        }

        [Button("選択"),ProgressBar(0,10)]
        public void Select(int index) {
            
            var slot = GetSelectedSlot(index);
            
            var spell = GetSlotInstance(slot);

            if (!GetSendable(spell)) {
                return;
            }
            
            SendSpellInstance(spell);
            
        }

        private ISelectorSlot GetSelectedSlot(int index) {
            
            if (index > Length || index < 0) {
                throw new ArgumentOutOfRangeException();
            }

            var result = Spells[index] ?? throw new NullReferenceException();

            return result;
        }

        private bool GetSendable(ISpellInstance spell) {
            
            if (spell is IMainSpellInstance) {
                return m_mainSendable.Sendable;
            }
            if (spell is ISubSpellInstance) {
                return m_subSendable.Sendable;
            }
            return false;
        }

        private ISpellInstance GetSlotInstance(ISelectorSlot slot) {

            if (slot is null) {
                throw new NullReferenceException();
            }

            if (slot.IsEmpty is false) {
                throw new ArgumentNullException();
            }
            
            return slot.Spell.CurrentValue;
        }

        private void SendSpellInstance(ISpellInstance spell) {
            m_publisher.Publish(new OnSelectEventBus(spell));
        }

        private void DeleteSpellInstance() {
            
        }
    }
}