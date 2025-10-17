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

        private GameObject m_entity;
        
        private ISubscriber<ITakeDamageEventBus> m_takeDamageSubscriber;
        
        private IDisposable m_subscription;

        [Inject]
        public DamageModule(ISubscriber<ITakeDamageEventBus> subscriber, IHealth health, GameObject entity) {
            
            m_health = health ?? throw new ArgumentNullException(nameof(health));
            
            m_takeDamageSubscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));
            
            m_entity = entity ?? throw new ArgumentNullException(nameof(entity));
            
            m_subscription = m_takeDamageSubscriber.Subscribe(OnDamage);
            
        }

        public void Dispose() {
            m_subscription.Dispose();
        }

        private void OnDamage(ITakeDamageEventBus eventBus) {
            
        }
    }
}