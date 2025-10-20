using MessagePipe;
using Sirenix.OdinInspector;
using Src.Health;
using Src.Health.EventBus;
using UnityEngine;
using VContainer;

namespace Mock.Debug {
    public class DebugDamageController : SerializedMonoBehaviour {
        
        private IObjectResolver m_resolver;
        
        private IPublisher<ITakeDamageEventBus> m_publisher;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_publisher = m_resolver.Resolve<IPublisher<ITakeDamageEventBus>>();
        }

        [Button("ダメージ生成")]
        public void CreateDamage(GameObject target, int value) {
            m_publisher.Publish(new TakeDamageEventBus(target, new Damage(value)));
        }
        
    }
}