using System;
using MessagePipe;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health.EventBus;
using UnityEngine;
using VContainer;

namespace Src.Health {

    public interface IDamageable {
        
    }
    
    [Serializable]
    public class DamageModule : IDamageable, IDisposable {

        [OdinSerialize]
        [ReadOnly]
        private IHealth m_health;

        [SerializeField]
        [ReadOnly]
        private GameObject m_entity;
        
        private ISubscriber<ITakeDamageEventBus> m_takeDamageSubscriber;
        
        private IDisposable m_subscription;
        
        [Inject]
        public DamageModule(ISubscriber<ITakeDamageEventBus> subscriber, IHealth health, GameObject entity) {
            
            m_health = health ?? throw new ArgumentNullException(nameof(health));
            
            m_takeDamageSubscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));

            if (m_health is MonoBehaviour mono) {
                m_entity = mono.gameObject;
            }
            else {
                Debug.Log("HealthからのGameObject取得ができませんでした");
                m_entity = entity;
            }
            
            Debug.Log($"{m_entity.transform.root}のダメージメッセージ登録処理を開始します");
            
            m_subscription = m_takeDamageSubscriber.Subscribe(OnDamage);
            
        }

        public void Dispose() {
            Debug.Log($"{m_entity.transform.root.name}のdamageModule.Disposeが呼ばれました");
            m_subscription.Dispose();
        }

        private void OnDamage(ITakeDamageEventBus eventBus) {
            
            Debug.Log($"{eventBus.Object.transform.root.name}に対するダメージ通知が{m_entity.transform.root.name}に届きました");

            if (m_entity.transform.root.IsChildOf(eventBus.Object.transform) is false) {
                Debug.Log($"{m_entity.transform.root.transform.name}とは別のターゲットのダメージですので処理を中断します");
                return;
            }
            
            Debug.Log($"{m_entity.gameObject.name}が{eventBus.Damage.Value}のダメージを受けました");
            
            var value = eventBus.Damage.Value;
            
            m_health.Decrease(value);
            
        }
    }
}