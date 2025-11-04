using System;
using MessagePipe;
using R3;
using RinaStatus;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health.EventBus;
using Src.Sound;
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

        [SerializeField]
        [LabelText("被弾音")]
        private AudioClip m_onDamageClip;
        
        [SerializeField]
        [LabelText("回復音")]
        private AudioClip m_onHealClip;
        
        [Title("参照")]
        
        [OdinSerialize]
        [ReadOnly]
        private IMaxHealth m_maxHealth;

        private IPublisher<IEmitSoundEventBus> m_onEmitPublisher;
        
        private IPublisher<IOnDeadEventBus> m_OnDeadPublisher;
        
        private ISubscriber<ITakeDamageEventBus> m_takeDamageSubscriber;
        
        private IDisposable m_takeDamageSubscription;
        
        private ISubscriber<IHealEventBus> m_healSubscriber;
        
        private IDisposable m_healSubscription;

        protected override void Start() {
            
            base.Start();
            
            m_OnDeadPublisher = m_resolver.Resolve<IPublisher<IOnDeadEventBus>>() 
                                ?? throw new ArgumentNullException();

            m_onEmitPublisher = m_resolver.Resolve<IPublisher<IEmitSoundEventBus>>()
                                ?? throw new ArgumentNullException();
            
            m_maxHealth = m_resolver.Resolve<IMaxHealth>() 
                          ?? throw new ArgumentNullException();

            m_takeDamageSubscriber = m_resolver.Resolve<ISubscriber<ITakeDamageEventBus>>()
                                     ?? throw new ArgumentNullException();

            m_takeDamageSubscription = m_takeDamageSubscriber.Subscribe(OnTakeDamage);
            
            m_healSubscriber = m_resolver.Resolve<ISubscriber<IHealEventBus>>()
                               ?? throw new ArgumentNullException();
            
            m_healSubscription = m_healSubscriber.Subscribe(OnHeal);
            
            m_rawValue.Set(m_initValue);
            
            RegisterValueChange();
            
            RegisterMaxValueChange();
            
        }

        protected void OnDestroy() {
            m_takeDamageSubscription?.Dispose();
            m_healSubscription?.Dispose();
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

        private void OnTakeDamage(ITakeDamageEventBus bus) {
            
            if (m_onDamageClip is null) return;
            
            m_onEmitPublisher?.Publish(new EmitSoundEventBus(m_onDamageClip));
            
        }

        private void OnHeal(IHealEventBus bus) {
            
            if (m_onHealClip is null) return;
            
            m_onEmitPublisher?.Publish(new EmitSoundEventBus(m_onHealClip));
            
        }

    }
}