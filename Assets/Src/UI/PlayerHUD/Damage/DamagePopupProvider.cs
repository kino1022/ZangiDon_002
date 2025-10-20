using System;
using MessagePipe;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health.EventBus;
using UnityEngine;
using VContainer;

namespace Src.UI.PlayerHUD.Damage {
    public interface IDamagePopupProvider {
        
    }

    public class DamagePopupProvider : SerializedMonoBehaviour, IDamagePopupProvider {
        
        [OdinSerialize]
        [ReadOnly]
        private IDamagePopupFactory m_popupFactory;
        
        private IObjectResolver m_resolver;
        
        private ISubscriber<ITakeDamageEventBus> m_subscriber;
        
        private IDisposable m_subscription;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_subscriber = m_resolver.Resolve<ISubscriber<ITakeDamageEventBus>>();
            m_popupFactory = m_resolver.Resolve<IDamagePopupFactory>();
            m_subscription = m_subscriber.Subscribe(OnTakeDamage);
        }

        private void OnDestroy() {
            m_subscription.Dispose();
        }

        private void OnTakeDamage(ITakeDamageEventBus eventBus) {
            m_popupFactory.Create(eventBus);
        }
    }
}