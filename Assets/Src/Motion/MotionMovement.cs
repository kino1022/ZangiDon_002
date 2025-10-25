using System.Collections.Generic;
using Sirenix.OdinInspector;
using Src.Move;
using Unity.VisualScripting;
using UnityEngine;

namespace Src.Motion {
    public class MotionMovement : SerializedStateMachineBehaviour {

        [SerializeField]
        [LabelText("進行度に対する運動量")]
        [InfoBox("クリップの開始を0、終了を1としてカーブを形成すること")]
        protected AnimationCurve m_moveForce;
        
        [SerializeField]
        [LabelText("運動方向")]
        protected Vector3 m_direction;
        
        [SerializeField]
        [LabelText("運動量の変化")]
        protected Dictionary<int, Vector3> m_changeDirection = new Dictionary<int, Vector3>();
        
        [Title("参照")]
        
        [LabelText("運動量マネージャ")]
        [ReadOnly]
        protected IMotionMoveManager m_moveManager;

        [Title("ランタイム")] 

        [SerializeField] [LabelText("現在の運動の強さ")] [ReadOnly]
        private float m_currentForce = 0.0f;
        
        [SerializeField]
        [LabelText("現在の運動方向")]
        [ReadOnly]
        private Vector3 m_currentDirection = Vector3.zero;
        
        [SerializeField]
        [LabelText("フレームレート")]
        [ReadOnly]
        private float m_frameRate = 0.0f;
        
        [SerializeField]
        [LabelText("現在フレーム")]
        [ReadOnly]
        private int m_currentFrame = 0;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
            m_frameRate = animator.GetFrameRate(layerIndex);
            
            m_moveManager = animator.GetComponentFromContainer<IMotionMoveManager>() ?? throw new MissingComponentException("MotionMoveManager");
            
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            m_currentFrame = stateInfo.GetCurrentFrame(m_frameRate);
            
            UpdateForce(stateInfo);

            UpdateDirection();

            m_moveManager.SetForce(m_currentForce);
            
            m_moveManager.SetDirection(m_currentDirection);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            m_moveManager.SetForce(0.0f);
            m_moveManager.SetDirection(Vector3.zero);
        }

        private void UpdateForce(AnimatorStateInfo stateInfo) {
            var progress = stateInfo.normalizedTime;
            m_currentForce = m_moveForce.Evaluate(progress);
        }

        private void UpdateDirection() {
            
            if (m_changeDirection is not null || m_changeDirection.Count is not 0) {
                if (m_changeDirection.TryGetValue(m_currentFrame, out var direction)) {
                    m_currentDirection = direction.normalized;
                }
            }
            
        }
    }
}