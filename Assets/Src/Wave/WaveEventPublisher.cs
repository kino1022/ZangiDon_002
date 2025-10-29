using System;
using Cysharp.Threading.Tasks;
using MessagePipe;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.GameManager.Entities;
using Src.Wave.EventBus;
using UnityEngine;
using VContainer;

namespace Src.Wave {

    public interface IWaveEventPublisher {
        
    }

    public class WaveEventPublisher : SerializedMonoBehaviour, IWaveEventPublisher {
        
        [Title("ランタイム")]
        
        [SerializeField]
        [LabelText("現在ウェーブ")]
        [ReadOnly]
        private int m_currentWave = 0;

        [SerializeField]
        [LabelText("待機中か")]
        [ReadOnly]
        private bool m_isWaiting = false;

        [SerializeField]
        [LabelText("待機時間(秒)")]
        private float m_waitSecond = 3.0f;

        private IPublisher<IWaveStartEventBus> m_publisher;
        
        [Title("参照")]
        
        [OdinSerialize]
        [LabelText("エンティティ管理クラス")]
        [ReadOnly]
        private IEntitiesProvider m_entitiesProvider;
        
        private CompositeDisposable m_isObserve;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            
            m_isObserve = new CompositeDisposable();
            
            m_entitiesProvider = m_resolver.Resolve<IEntitiesProvider>();
            
            m_publisher = m_resolver.Resolve<IPublisher<IWaveStartEventBus>>();
            
            RegisterEntitiesEmpty();
            
        }

        private void RegisterEntitiesEmpty() {
            
            m_isObserve?.Dispose();
            
            m_isObserve = new CompositeDisposable();
            
            Observable
                .EveryValueChanged(m_entitiesProvider, x => x.Entities.Count)
                .Where(x => x == 0)
                .Subscribe(_ => {
                    
                    if (m_isWaiting is true) return;
                    
                    m_isWaiting = true;
                    AsyncWait().Forget();
                        
                })
                .AddTo(m_isObserve);
        }

        private async UniTask AsyncWait() {
            try {
                
                await UniTask.Delay(
                    TimeSpan.FromSeconds(m_waitSecond),
                    cancellationToken: this.GetCancellationTokenOnDestroy()
                );

                m_isWaiting = false;
                m_currentWave++;
                m_publisher.Publish(new WaveStartEventBus(m_currentWave));

                RegisterEntitiesEmpty();
                
            }
            catch (OperationCanceledException) {

            }
            finally {
                m_isObserve?.Dispose();
            }
        }
    }
}