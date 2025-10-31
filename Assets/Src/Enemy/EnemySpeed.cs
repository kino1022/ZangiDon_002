using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Src.Enemy {

    public interface IEnemySpeed {
        
        ReadOnlyReactiveProperty<float> Speed { get; }
        
    }
    
    public class EnemySpeed : SerializedMonoBehaviour, IEnemySpeed {

        [SerializeField]
        [LabelText("初期速度")]
        private float m_initSpeed = 10;

        private ReactiveProperty<float> m_speed;
        
        public ReadOnlyReactiveProperty<float> Speed => m_speed;

        private void Awake() {
            m_speed = new ReactiveProperty<float>(m_initSpeed);
        }
    }
}