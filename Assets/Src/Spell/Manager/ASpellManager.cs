using System;
using System.Collections.Generic;
using ObservableCollections;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Slot.Factory.Interface;
using Src.Spell.Slot.Interface;
using UnityEngine;
using VContainer;

namespace Src.Spell.Manager {
    /// <summary>
    /// スペルを管理するクラスの基底クラス
    /// </summary>
    /// <typeparam name="Instance">管理するスペルの型</typeparam>
    /// <typeparam name="Slot">管理に利用するスロットの型</typeparam>
    public abstract class ASpellManager<Instance,Slot> : SerializedMonoBehaviour, ISpellManager<Instance,Slot> 
        where Instance : ISpellInstance 
        where Slot : ISpellSlot<Instance> 
    {

        [OdinSerialize, LabelText("管理しているスペル")]
        protected ObservableDictionary<int, Slot> m_spells;

        [SerializeField, LabelText("管理できる量"),ProgressBar(0,10)]
        private int m_length = 0;
        
        [OdinSerialize]
        [LabelText("管理スロット")]
        [ReadOnly]
        private Dictionary<Slot,CompositeDisposable> m_disposables = new Dictionary<Slot, CompositeDisposable>();

        [TitleGroup("参照")]
        [OdinSerialize, LabelText("スロット生成クラス"), ReadOnly]
        protected ISlotFactory<Instance, Slot> m_slotFactory;

        protected IObjectResolver m_resolver;
        
        public IReadOnlyObservableDictionary<int, Slot> Spells => m_spells;
        
        public int Length => m_length;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException();
        }

        protected virtual void Start() {
            
            m_slotFactory = m_resolver.Resolve<ISlotFactory<Instance,Slot>>() ?? throw new ArgumentNullException();
            
            InitializeSpellsDictionary();
        }

        protected virtual void InitializeSpellsDictionary() {
            
            m_spells = new ObservableDictionary<int, Slot>();
            
            for (int i = 0; i < Length; i++) {
                m_spells.Add(i, m_slotFactory.Create());
            }
        }

        public virtual void Add(Instance spell) {
            
            var targetSlot = GetEmptySlot() ?? throw new NullReferenceException();
            
            targetSlot.Set(spell);
            
        }

        public virtual void Remove(int index) {

            if (index < 0 || index >= Length) {
                throw new ArgumentOutOfRangeException();
            }
            
            var targetSlot = m_spells[index] ?? throw new NullReferenceException();
            targetSlot.Remove();
        }

        /// <summary>
        /// 空いているスロットの中で一番若いものを返す
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected virtual Slot GetEmptySlot() {
            
            foreach (var slot in m_spells) {
                if (slot.Value.IsEmpty) {
                    return slot.Value;
                }
            }
            
            throw new NullReferenceException();
        }

        protected void RegisterSlotObserve(Slot slot) {
            
            if (slot is null) throw new NullReferenceException();
            
            m_disposables.Add(slot, new CompositeDisposable());
            
        }

        private void RegisterSpellAmount(Slot slot, CompositeDisposable disposables) {
            
            if (slot is null) throw new NullReferenceException();

            var amountModule = slot.Spell.CurrentValue.Amount;
            
            amountModule
                .Amount
                .Subscribe(x => {
                    if (x <= 0) {
                        slot.Remove();
                    }
                })
                .AddTo(disposables);
        }
    }
}