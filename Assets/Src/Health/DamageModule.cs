using System;
using MessagePipe;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health.EventBus;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using DisposableBag = MessagePipe.DisposableBag;

namespace Src.Health {

    public interface IDamageable {
        
    }

    [Serializable]
    public class DamageModule : IDamageable, IStartable, IDisposable {

        [Title("参照")]

        [OdinSerialize]
        [LabelText("体力コンポーネント")]
        [ReadOnly]
        private IHealth m_health;

        [SerializeField]
        [LabelText("持ち主")]
        [ReadOnly]
        private GameObject m_entity;

        [OdinSerialize]
        [LabelText("サブスクライバー")]
        [ReadOnly]
        private ISubscriber<ITakeDamageEventBus> m_subscriber;

        private IDisposable m_subscription;
        

        private IObjectResolver m_resolver;

        public DamageModule(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        public void Dispose() {
            m_subscription?.Dispose();
        }

        public void Start() {
            m_health = m_resolver.Resolve<IHealth>() ?? throw new ArgumentNullException(nameof(m_health));

            if (m_health is MonoBehaviour mono) {
                m_entity = mono.gameObject;
            }

            m_subscriber = m_resolver.Resolve<ISubscriber<ITakeDamageEventBus>>();

            m_subscription = m_subscriber.Subscribe(OnTakeDamage);
        }

        private void OnTakeDamage (ITakeDamageEventBus eventBus) {

            if (eventBus is null) throw new ArgumentNullException (nameof(eventBus));

            Debug.Log($"{m_entity.transform.root.name}が{eventBus.Object.transform.root.name}に対してのダメージ通知を受け取りました");

            if (m_entity.transform.root.gameObject != eventBus.Object.transform.root.gameObject) {
                Debug.Log($"{eventBus.Object.transform.root}に対するダメージを受け取りましたが、{m_entity.transform.root.name}とは異なるので処理を中断します");
                return;
            }

            m_health.Decrease(eventBus.Damage.Value);
        }
    }
}