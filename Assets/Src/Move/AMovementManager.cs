using Sirenix.OdinInspector;
using UnityEngine;

namespace Src.Move {
    public abstract class AMovementManager : SerializedMonoBehaviour, IMovementManager , IForceProvider, IDirectionProvider {

        [SerializeField]
        [LabelText("運動量")]
        [ReadOnly]
        private float m_force;

        [SerializeField]
        [LabelText("方向")]
        [ReadOnly]
        private Vector3 m_direction;

        [ShowInInspector]
        public Vector3 Movement => m_direction.normalized * m_force;
        
        public float Force => m_force;
        
        public Vector3 Direction => m_direction;

        public void SetForce(float next) {
            
            //値が負の値なら方向を反転させて正の値に修正
            if (next < 0.0f) {
                next *= -1.0f;
                m_direction *= -1.0f;
            }
            
            m_force = next;
        }

        public void SetDirection(Vector3 next) {
            
            m_direction = next.normalized;
            
        }
    }
}