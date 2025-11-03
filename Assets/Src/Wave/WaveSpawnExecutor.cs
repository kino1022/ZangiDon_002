using System;
using System.Collections.Generic;
using GeneralModule.Symbol;
using MessagePipe;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spawner;
using Src.Wave.EventBus;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Src.Wave {
    public class WaveSpawnExecutor : SerializedMonoBehaviour {
        
        [Title("データ")]
        
        [OdinSerialize]
        [TableList]
        [LabelText("デフォルトスポーンデータ")]
        private List<IWaveSpawnData> m_defaultSpawnData = new List<IWaveSpawnData>();

        [OdinSerialize]
        [TableList]
        [LabelText("ウェーブ毎のスポーン")]
        private Dictionary<int, List<IWaveSpawnData>> m_datas = new();
        
        [Title("参照")]
        
        [OdinSerialize]
        [LabelText("スポーン実行")]
        [ReadOnly]
        private ISpawnExecutor m_executor;
        
        private IObjectResolver m_resolver;
        
        private ISubscriber<IWaveStartEventBus> m_subscriber;
        
        private IDisposable m_subscription;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            
            m_subscriber = m_resolver.Resolve<ISubscriber<IWaveStartEventBus>>();
            
            m_subscription = m_subscriber.Subscribe(OnWaveChange);
            
            m_executor = m_resolver.Resolve<ISpawnExecutor>();
            
        }

        private void OnDestroy() {
            m_subscription?.Dispose();
        }

        private void OnWaveChange(IWaveStartEventBus eventBus) {
            var wave = eventBus.WaveCount;

            // 辞書にキーが無い場合に例外が飛ばないよう TryGetValue を使用
            if (!m_datas.TryGetValue(wave, out var datas) || datas == null || datas.Count == 0) {
                // デフォルトにフォールバック
                datas = m_defaultSpawnData ?? new List<IWaveSpawnData>();
            }

            // デフォルトも空なら何もしない
            if (datas.Count == 0) {
                Debug.Log("取得したスポーンデータが空でした");
                return;
            }

            foreach (var data in datas) {
                if (data == null) continue;

                var amount = Random.Range(data.Min, data.Max);

                for (int i = 0; i < amount; ++i) {
                    m_executor.SpawnSymbol(data.Symbol);
                }
            }
        }
    }

    public interface IWaveSpawnData {
        int Max { get; }
        
        int Min { get; }
        
        ASerializedSymbol Symbol { get; }
    }

    [Serializable]
    public class WaveSpawnData : IWaveSpawnData {

        [SerializeField]
        [LabelText("最大数")]
        [ProgressBar(0,20)]
        private int m_max = 0;
        
        [SerializeField]
        [LabelText("最小数")]
        [ProgressBar(0, 20)]
        private int m_min = 0;
        
        [SerializeField]
        [LabelText("シンボル")]
        private ASerializedSymbol m_symbol;
        
        public int Max => m_max;
        
        public int Min => m_min;
        
        public ASerializedSymbol Symbol => m_symbol;
    }
}