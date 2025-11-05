using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Src.Move.Inertial {

    public interface IInertialManager : IMovementManager {
        
        void Add (IInertial inertial);
        
        void Remove (IInertial inertial);
        
        void Clear ();
        
    }
    
    public class InertialManager : SerializedMonoBehaviour, IInertialManager {
        
        [OdinSerialize]
        [LabelText("働いている慣性")]
        private List<IInertial> m_inertials = new();

        [SerializeField]
        [LabelText("慣性消去閾値")]
        private float m_threshold = 0.01f;
        
        [SerializeField]
        [LabelText("慣性の総量")]
        [ReadOnly]
        private Vector3 m_movement = Vector3.zero;
        
        public Vector3 Movement => m_movement;

        public void Add(IInertial inertial) {
            
            if (inertial is null) {
                throw new System.ArgumentNullException(nameof(inertial));
            }

            if (inertial.Movement.magnitude < m_threshold) {
                return;
            }
            
            m_inertials.Add(inertial);
        }
        
        public void Remove(IInertial inertial) => m_inertials.Remove(inertial);
        
        public void Clear() => m_inertials.Clear();

        private void FixedUpdate() {
            //不要な慣性の消去
            DeleteInertial();
            //慣性の更新
            m_movement = CalculateMovement();
        }

        private void DeleteInertial() {
            
            if (m_inertials.Count is 0 || m_inertials is null) {
                return;
            }
            
            for (int i = m_inertials.Count - 1; i >= 0; i--) {
                var inertial = m_inertials[i];
                if (inertial is null || inertial.Movement.magnitude < m_threshold) {
                    m_inertials.RemoveAt(i);
                    inertial?.Dispose();
                }
            }
        }

        private Vector3 CalculateMovement() {

            var result = Vector3.zero;
            
            if (m_inertials.Count is 0 || m_inertials is null) {
                return result;
            }
            
            m_inertials.ForEach(x => result += x.Movement);
            
            return result;
            
        }
    }
    
}