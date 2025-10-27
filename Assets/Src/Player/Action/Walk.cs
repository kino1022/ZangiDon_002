using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Control;
using Src.Move;
using UnityEngine;
using VContainer;

namespace Src.Player.Action {

    public class Walk : SerializedMonoBehaviour {

        [Title("設定")]

        [SerializeField]
        [LabelText("速度設定")]
        [InfoBox("入力量に対しての速度設定,入力無しを0,最大入力を1として設定する")]
        private AnimationCurve m_speedCurve;

        [Title("参照")]

        [OdinSerialize]
        [ReadOnly]
        private IInputDirectionProvider m_direction;

        [OdinSerialize]
        [ReadOnly]
        private IInputForceProvider m_force;

        [OdinSerialize]
        [ReadOnly]
        private IMotionMoveManager m_moveManager;

        [Title("ランタイムデータ")]

        [SerializeField]
        [LabelText("現在の方向")]
        [ReadOnly]
        private Vector3 m_currentDirction = Vector3.zero;

        [SerializeField]
        [LabelText("現在の速度")]
        [ReadOnly]
        private float m_currentSpeed = 0.0f;

        private IObjectResolver m_resolver;

        [Inject]
        public void Construct (IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_direction = m_resolver.Resolve<IInputDirectionProvider>();

            m_force = m_resolver.Resolve<IInputForceProvider>();

            m_moveManager = m_resolver.Resolve<IMotionMoveManager>();
        }

        private void FixedUpdate () {

            m_currentDirction = new Vector3 (m_direction.InputDirection.x, 0.0f, m_direction.InputDirection.y);

            m_currentSpeed = m_speedCurve.Evaluate(m_force.InputForce);

            m_moveManager.SetForce(m_currentSpeed);

            m_moveManager.SetDirection(m_currentDirction);
        }
    }

}