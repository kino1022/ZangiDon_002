using System;
using R3;
using RinaCorrection;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;

namespace Src.Enemy {

    public interface IEnemySpeed {
        
        ReadOnlyReactiveProperty<float> Speed { get; }
        
        ICorrectionManager Correction { get; }
        
    }
    
    public class EnemySpeed : SerializedMonoBehaviour, IEnemySpeed {

        [SerializeField]
        [LabelText("初期速度")]
        private float m_initSpeed = 10;

        private ReactiveProperty<float> m_speed;

        [OdinSerialize]
        [LabelText("補正値管理クラス")]
        [ReadOnly]
        private ICorrectionManager m_correction;

        public ICorrectionManager Correction => m_correction;
        
        public ReadOnlyReactiveProperty<float> Speed => m_speed;
        
        private IObjectResolver m_resolver;

        [Inject]
        private void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }
        
        private void Awake() {
            m_speed = new ReactiveProperty<float>(m_initSpeed);
        }

        private void Start() {
            m_correction = m_resolver.Resolve<ICorrectionManager>() ?? throw new NullReferenceException();

            m_correction
                .OnChanged
                .Subscribe(_ => {
                    Debug.Log("補正値の適用を行います");
                    
                    var correctedSpeed = m_correction.Apply(m_speed.CurrentValue);

                    if (correctedSpeed < 0.0f) {
                        correctedSpeed = 0.0f;
                    }
                    m_speed.Value = correctedSpeed;
                })
                .AddTo(this);
        }
    }
}