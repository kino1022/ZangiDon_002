using Src.Health.EventBus;

namespace Src.Health {
using System;
using MessagePipe;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using DisposableBag = MessagePipe.DisposableBag;

namespace Src.Health {

    public interface IHealable {
        
    }

    [Serializable]
    public class HealModule : IHealable, IStartable, IDisposable {

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
        private ISubscriber<IHealEventBus> m_subscriber;

        private IDisposable m_subscription;
        
        private IObjectResolver m_resolver;

        public HealModule(IObjectResolver resolver) {
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

            m_subscriber = m_resolver.Resolve<ISubscriber<IHealEventBus>>();

            m_subscription = m_subscriber.Subscribe(OnHeal);
        }

        private void OnHeal (IHealEventBus eventBus) {

            if (eventBus is null) throw new ArgumentNullException (nameof(eventBus));

            Debug.Log($"{m_entity.transform.root.name}が{eventBus.Target.transform.root.name}に対しての回復通知を受け取りました");

            if (m_entity.transform.root != eventBus.Target.transform.root) {
                Debug.Log($"{eventBus.Target.transform.root}に対する回復を受け取りましたが、{m_entity.transform.root.name}とは異なるので処理を中断します");
                return;
            }
            
            Debug.Log($"{m_entity.gameObject.name}の回復を実行します。");

            m_health.Increase(eventBus.Heal.Value);
        }
    }
}
}