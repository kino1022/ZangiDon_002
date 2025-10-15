using System;
using MessagePipe;
using R3;
using Src.Spell.EventBus.Interface;
using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Interface;
using UnityEngine;
using VContainer;

namespace Src.Spell.Manager {
    /// <summary>
    ///　選択済みのスペルを管理するクラスの基底クラス
    /// </summary>
    public abstract class ASelectedSpellManager<Instance, Slot> : ASpellManager<Instance, Slot> 
        where Instance : ISpellInstance
        where Slot : ISpellSlot<Instance>
    {
        
        protected ISubscriber<IOnSelectEventBus> m_subscriber;
        
        protected IDisposable m_subscription;
        

        protected override void Start() {
            base.Start();
            
            m_subscriber = m_resolver.Resolve<ISubscriber<IOnSelectEventBus>>() ?? throw new ArgumentNullException();

            //スペル選択時の購読処理
            m_subscription = m_subscriber.Subscribe(OnSpellSelected);
        }

        protected void OnDestroy() {
            //購読処理の終了
            m_subscription?.Dispose();
        }

        protected virtual void OnSpellSelected(IOnSelectEventBus eventBus) {
            
            //スペルが満タンな時点でアサート
            Debug.Assert(this.IsFull(),"スペルが満タンな状態でスペルが補充されそうになりました");

            if (eventBus.Spell is Instance spell) {
                var target = GetEmptySlot();
                target.Set(spell);
                
                OnSelectSpellAdded(spell);
            }
        }
        
        protected virtual void OnSelectSpellAdded (Instance spell) {}

        private void RegisterAmountZero(Slot slot) {
            slot.Spell.CurrentValue.Amount.Amount
                .Subscribe(x => {
                    if (x <= 0) {
                        slot.Remove();
                    }
                })
                .AddTo(this);
        }
    }
}