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
        [LabelText("待機時間(秒)")]
        private float m_waitSecond = 3.0f;

        private IPublisher<IWaveStartEventBus> m_publisher;
        
        [Title("参照")]
        
        [OdinSerialize]
        [LabelText("エンティティ管理クラス")]
        [ReadOnly]
        private IEntitiesProvider m_entitiesProvider;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            
            m_entitiesProvider = m_resolver.Resolve<IEntitiesProvider>();
            
            m_publisher = m_resolver.Resolve<IPublisher<IWaveStartEventBus>>();
            
            RegisterExterminate();
        }

        ///処理の流れとしては
        /// 1.Entities.count == 0になるまでの待機処理
        /// 2.await UniTask.Delay(TimeSpan.FromSeconds(m_interval))での待機処理
        /// 3.m_waveCount++
        /// 4.m_publisher.Publish(new WaveStartEventBus(m_waveCount))でのパブリッシュ
        /// 5.1へ戻る再起処理


        private void RegisterExterminate() {
            
            Observable
                .EveryValueChanged(m_entitiesProvider.Entities, x => x.Count)
                .Where(x => x == 0)
                .Subscribe(_ => {
                    
                    WaitNextWave().Forget();
                    
                })
                .AddTo(this);
            
        }

        private async UniTask WaitNextWave() {
            try {
                
                await UniTask.Delay(
                    TimeSpan.FromSeconds(m_waitSecond),
                    cancellationToken: this.GetCancellationTokenOnDestroy()
                    );

                m_currentWave++;
                
                m_publisher.Publish(new WaveStartEventBus(m_currentWave));

            }
            catch (OperationCanceledException) {
                
            }
        }
    }
}