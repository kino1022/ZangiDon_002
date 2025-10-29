using System;
using MessagePipe;
using Sirenix.OdinInspector;
using Src.Health.EventBus;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace Src.Player {
    public class OnDeadCallBackManager : SerializedMonoBehaviour {

        [SerializeField]
        [LabelText("死亡時に呼び出されるイベント")]
        private UnityEvent m_deadEvents = new();
        
        private ISubscriber<IOnDeadEventBus> m_OnDeadEventBus;
        
        private IDisposable m_subscription;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            
            m_OnDeadEventBus = m_resolver.Resolve<ISubscriber<IOnDeadEventBus>>();

            m_subscription = m_OnDeadEventBus.Subscribe(OnTakeEventBus);
            
        }

        private void OnDestroy() {
            m_subscription?.Dispose();
        }

        private void OnTakeEventBus(IOnDeadEventBus eventBus) {

            if (gameObject.transform.root.IsChildOf(eventBus.Object.transform) is false) {
                Debug.Log("異なるキャラクターの死亡通知を受け取ったため処理を中断します");
                return;
            }
            
            m_deadEvents?.Invoke();
            
            Destroy(gameObject);
        }
        
    }
}