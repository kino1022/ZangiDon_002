using System;
using MessagePipe;
using R3;
using RinaStatus;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health.EventBus;
using UnityEngine;
using VContainer;

namespace Src.Health {
    
    public interface IHealth : IStatus<int> {
        
    }
    
    [DefaultExecutionOrder(1000)]
    public class Health : AStatus<int> , IHealth {

        [Title("設定")] 
        
        [SerializeField]
        [LabelText("初期値")]
        private int m_initValue = 100;
        
        [Title("参照")]
        
        [OdinSerialize]
        [ReadOnly]
        private IMaxHealth m_maxHealth;
        
        private IPublisher<IOnDeadEventBus> m_OnDeadPublisher;

        protected override void Start() {
            
            base.Start();
            
            m_rawValue.Set(m_initValue);
            
            m_OnDeadPublisher = m_resolver.Resolve<IPublisher<IOnDeadEventBus>>() 
                                ?? throw new ArgumentNullException();
            
            m_maxHealth = m_resolver.Resolve<IMaxHealth>() 
                          ?? throw new ArgumentNullException();
            
            RegisterValueChange();
            
            RegisterMaxValueChange();
            
        }

        private void RegisterValueChange() {
            Value
                .Subscribe(x => {
                    
                    Debug.Log("体力の変化を検知しました");
                    
                    //体力がO以下なら死亡処理
                    if (x <= 0) {
                        Debug.Log("体力が0以下になったので処理を発火します");
                        OnDead();
                        return;
                    }

                    if (m_maxHealth.Value.CurrentValue < Value.CurrentValue) {
                        OnOverMax();
                    }
                    
                })
                .AddTo(this);
        }

        private void RegisterMaxValueChange() {
            Value
                .Subscribe(x => {
                    //最大値が変化して現在値を下回った場合
                    if (x < Value.CurrentValue) {
                        OnOverMax();
                    }
                })
                .AddTo(this);
        }

        private void OnDead() {
            //死亡していることの通知処理
            m_OnDeadPublisher?.Publish(new OnDeadEventBus(gameObject.transform.root.gameObject));
        }

        private void OnOverMax() {
            //最大値で体力を初期化
            Set(m_maxHealth.Value.CurrentValue);
        }

    }
}