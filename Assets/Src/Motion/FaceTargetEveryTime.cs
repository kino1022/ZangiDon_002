using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Target;
using UnityEngine;
using VContainer;

namespace Src.Motion {
    public class FaceTargetEveryTime : SerializedMonoBehaviour {

        [Title("設定")] 
        
        [SerializeField] 
        [LabelText("向き直る速さ")]
        private float m_faceSpeed = 20.0f;

        [SerializeField]
        [LabelText("ダミー")] 
        private Transform m_defaultTransform;
        
        [Title("ランタイム")]
        
        [SerializeField]
        [LabelText("向き直る対象")]
        [ReadOnly]
        private Transform m_faceTarget;
        
        [Title("参照")]
        
        [OdinSerialize]
        [LabelText("ターゲット供給クラス")]
        [ReadOnly]
        private ITargetProvider m_targetProvider;

        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            
            m_targetProvider = m_resolver.Resolve<ITargetProvider>();
            
            RegisterChangeTarget();
            
            CreateUpdateStream();
        }

        private void CreateUpdateStream() {
            Observable
                .EveryUpdate()
                .Subscribe(_ => {
                    
                    var nextDir = CalculateDirection();
                    
                    transform.rotation = Quaternion.Slerp(
                        Quaternion.LookRotation(nextDir),
                        gameObject.transform.rotation,
                        m_faceSpeed * Time.deltaTime
                    );
                    
                })
                .AddTo(this);
        }

        private void RegisterChangeTarget() {
            m_targetProvider
                .Target
                .Subscribe(x => {
                    
                    if (x is null) {
                        m_faceTarget = m_defaultTransform;
                        return;
                    }
                    
                    m_faceTarget = x.transform;
                })
                .AddTo(this);
        }

        private Vector3 CalculateDirection() {
            var targetPos = m_faceTarget.position;
            targetPos.y = 0;
            
            var playerPos = gameObject.transform.position;
            playerPos.y = 0;
            
            return targetPos - playerPos;
        }
    } 
}