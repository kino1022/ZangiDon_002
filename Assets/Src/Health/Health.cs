using System;
using GeneralModule.Status;
using GeneralModule.Status.Interface;
using MessagePipe;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health.EventBus;
using Unity.VisualScripting;
using VContainer;

namespace Src.Health {
    
    public interface IHealth : IStatus<int> {
        
    }
    
    public class Health : AStatus<int> , IHealth {
        
        [OdinSerialize]
        [ReadOnly]
        private IMaxHealth m_maxHealth;
        
        private IPublisher<IOnDeadEventBus> m_OnDeadPublisher;

        protected override void Start() {
            
            m_OnDeadPublisher = m_resolver.Resolve<IPublisher<IOnDeadEventBus>>() 
                                ?? throw new ArgumentNullException();
            
            m_maxHealth = m_resolver.Resolve<IMaxHealth>() 
                          ?? throw new ArgumentNullException();
            
            RegisterValueChange();
            
            RegisterMaxValueChange();
            
        }

        private void RegisterValueChange() {
            Observable
                .EveryValueChanged(this, x => x.Value)
                .Subscribe(x => {
                    
                    //体力がO以下なら脂肪処理
                    if (x <= 0) {
                        OnDead();
                    }

                    if (m_maxHealth.Value < Value) {
                        OnOverMax();
                    }
                })
                .AddTo(this);
        }

        private void RegisterMaxValueChange() {
            Observable
                .EveryValueChanged(m_maxHealth, x => x.Value)
                .Subscribe(x => {
                    //最大値が変化して現在値を下回った場合
                    if (x < Value) {
                        OnOverMax();
                    }
                })
                .AddTo(this);
        }

        private void OnDead() {
            //死亡していることの通知処理
            m_OnDeadPublisher?.Publish(new OnDeadEventBus(gameObject));
        }

        private void OnOverMax() {
            //最大値で体力を初期化
            Set(m_maxHealth.Value);
        }

    }
}