using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Move.Inertial;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Src.Move {

    public interface ICharacterMoveController {
        
    }
    
    public class CharacterMoveController : SerializedMonoBehaviour, ICharacterMoveController {

        [OdinSerialize]
        [LabelText("運動量管理クラス")]
        private List<IMovementProvider> m_movementManagers = new();
        
        [SerializeField]
        [ReadOnly]
        private CharacterController m_cController;

        private Vector3 m_currentMovement;

        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_cController = m_resolver.Resolve<CharacterController>();
        }

        private void FixedUpdate() {

            m_currentMovement = CalculateTotalMovement();

            m_cController.Move(m_currentMovement * Time.fixedDeltaTime);
            
        }

        private Vector3 CalculateTotalMovement () {

            if (m_movementManagers is null || m_movementManagers.Count is 0) {
                return Vector3.zero;
            }

            var result = Vector3.zero;

            m_movementManagers.ForEach(x => result += x.Movement);

            return result;
        }
        
    }
}