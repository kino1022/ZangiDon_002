using System.Collections.Generic;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Target;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace Src.Camera {
    public class LockTargetSetter : SerializedMonoBehaviour {

        [Title("参照")] 
        
        [SerializeField]
        [LabelText("利用するカメラ")]
        private List<CinemachineCamera> m_cams = new();

        [SerializeField]
        [LabelText("デフォルトで見る方向")]
        private Transform m_defaultTransform;
        
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
        }

        private void RegisterChangeTarget() {
            m_targetProvider
                .Target
                .Subscribe(x => {
                    m_cams.ForEach(c => {
                        if (x is not null) {
                            c.LookAt = x.transform;
                        }
                        else {
                            c.LookAt = m_defaultTransform;
                        }
                    });
                });
        }
    }
}