using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Target;
using UnityEngine;
using VContainer;

namespace Src.Shoot {
    public class MuzzlePositionController : SerializedMonoBehaviour {

        [Title("設定")] 
        
        [SerializeField]
        [LabelText("プレイヤーからの距離")]
        private float m_range = 1.0f;

        [SerializeField]
        [LabelText("高さ")]
        private float m_height = 1.5f;
        
        [SerializeField]
        [LabelText("デフォルトの方向")]
        private Transform m_defaultDirection;
        
        [SerializeField]
        [LabelText("プレイヤーの位置")]
        private Transform m_player;

        [Title("ランタイム")] 
        
        [SerializeField]
        [ReadOnly]
        private Transform m_targetPosition;

        [Title("参照")]
        
        [OdinSerialize]
        [ReadOnly]
        private ITargetProvider m_targetProvider;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver  = resolver;
        }

        private void Start() {
            
            m_targetProvider = m_resolver.Resolve<ITargetProvider>();
            
        }

        private void Update() {
            
            if (m_targetProvider.Target.CurrentValue is null) {
                m_targetPosition = m_defaultDirection;

            }
            else {
                m_targetPosition = m_targetProvider.Target.CurrentValue.transform;
            }
            
            var nextPos = CalculateNextPosition() + m_player.transform.position;
            
            transform.position = nextPos;
            
        }

        private Vector3 CalculateNextPosition() {
            
            var dir = m_targetPosition.position - m_player.position;
            
            dir.y = 0.0f;
            
            var result = dir.normalized * m_range;

            result.y = m_height;
            
            return result;
        }
        
    }
}