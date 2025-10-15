using System;
using MessagePipe;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Container.Sendable.Interface;
using Src.Spell.EventBus;
using Src.Spell.EventBus.Interface;
using Src.Spell.Instance.Interface;
using Src.Spell.Instance.Main.Interface;
using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Manager.Selector.Interface;
using Src.Spell.Slot.Selector.Interface;
using Src.Spell.Supplier.Interface;
using VContainer;

namespace Src.Spell.Manager.Selector {
    public class SpellSelector : ASpellManager<ISpellInstance,ISelectorSlot> , ISpellSelector {

        [Title("参照")]
        
        [OdinSerialize]
        [LabelText("初期化モジュール")]
        private ISpellSelectorInitializer m_initializer;

        [OdinSerialize]
        [LabelText("メイン供給可否マネージャ")]
        private IMainSendableManager m_mainSendable;
        
        [OdinSerialize]
        [LabelText("サブ供給可否マネージャ")]
        private ISubSendableManager m_subSendable;
        
        private IPublisher<IOnSelectEventBus> m_publisher;
        
        [OdinSerialize]
        [LabelText("スペル供給クラス")]
        private ISpellSupplier m_supplier;

        protected override void Start() {
            
            base.Start();
            
            m_mainSendable = m_resolver.Resolve<IMainSendableManager>();
            m_subSendable = m_resolver.Resolve<ISubSendableManager>();
            m_publisher = m_resolver.Resolve<IPublisher<IOnSelectEventBus>>();
            m_supplier = m_resolver.Resolve<ISpellSupplier>();
            
            m_initializer.Initialize(this);
        }

        [Button("選択"),ProgressBar(0,10)]
        public void Select(int index) {
            
            var slot = GetSelectedSlot(index);
            
            var spell = GetSlotInstance(slot);

            if (!GetSendable(spell)) {
                return;
            }
            
            SendSpellInstance(spell);
            
            slot.Remove();

        }

        [Button("スペル補充")]
        public void Supply() {
            try {
                var slot = GetEmptySlot() ?? throw new NullReferenceException();
                var spell = m_supplier.Supply() ?? throw new NullReferenceException();
                slot.Set(spell);
            }
            catch (NullReferenceException e) {
                return;
            }
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

            if (slot.IsEmpty) {
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