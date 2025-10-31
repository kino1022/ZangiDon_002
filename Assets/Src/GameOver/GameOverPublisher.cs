using System;
using System.Collections.Generic;
using MessagePipe;
using Sirenix.OdinInspector;
using Src.Health.EventBus;
using Src.Wave.EventBus;
using UnityEngine;
using VContainer;

namespace Src.GameOver {
    public class GameOverPublisher : SerializedMonoBehaviour {
        
        [Title("設定")]

        [SerializeField] 
        [LabelText("敗北条件になるオブジェクト")]
        private List<GameObject> m_gameOverCondition = new();

        [Title("ランタイム")]
        
        [SerializeField] 
        [ReadOnly]
        private int m_currentWave = 0;
        
        private IPublisher<IGameOverEventBus> m_publisher;
        
        private ISubscriber<IOnDeadEventBus> m_subscriber;
        
        private ISubscriber<IWaveStartEventBus> m_waveSubscriber;
        
        private IDisposable m_subscription;
        
        private IDisposable m_waveSubscription;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            
            m_publisher = m_resolver.Resolve<IPublisher<IGameOverEventBus>>();
            
            m_subscriber = m_resolver.Resolve<ISubscriber<IOnDeadEventBus>>();
            
            m_waveSubscriber = m_resolver.Resolve<ISubscriber<IWaveStartEventBus>>();

            m_subscription = m_subscriber.Subscribe(OnTakeEventBus);

            m_waveSubscription = m_waveSubscriber.Subscribe(OnTakeEventBus);
        }

        private void OnDestroy() {
            
            m_subscription?.Dispose();
            
            m_waveSubscription?.Dispose();
            
        }

        private void OnTakeEventBus(IOnDeadEventBus eventBus) {

            var deadObject = eventBus.Object.transform.root;

            foreach (var condition in m_gameOverCondition) {
                if (deadObject.IsChildOf(condition.transform)) {
                    Debug.Log("敗北条件に当たるクラスが死亡したためゲームオーバーのイベントを発行します");
                    m_publisher.Publish(new GameOverEventBus(m_currentWave));
                }
            }
        }

        private void OnTakeEventBus(IWaveStartEventBus eventBus) {
            m_currentWave = eventBus.WaveCount;
        }
    }
}