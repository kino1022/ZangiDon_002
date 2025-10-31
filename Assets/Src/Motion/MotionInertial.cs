using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Move.Inertial;
using Src.Utility;
using UnityEngine;
using VContainer.Unity;
using VContainer;

namespace Src.Motion {
    public class MotionInertial : SerializedStateMachineBehaviour {
        
        [OdinSerialize]
        [LabelText("慣性と生成タイミング")]
        private Dictionary<float, List<IInertial>> m_inertials = new Dictionary<float, List<IInertial>>();
        
        [OdinSerialize]
        [ReadOnly]
        [LabelText("慣性マネージャ")]
        private IInertialManager m_inertialManager;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            
            var container = ComponentsUtility.GetComponentFromWhole<LifetimeScope>(animator.gameObject);
            
            m_inertialManager = container.Container.Resolve<IInertialManager>();
            
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            
            float progress = stateInfo.normalizedTime;

            var inertials = m_inertials[progress];

            if (inertials.Count is 0 || inertials is null) {
                return;
            }

            foreach (var inertial in inertials) {
                m_inertialManager.Add(inertial);
            }
            
        }
        
    }
}