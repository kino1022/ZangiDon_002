using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Move.Inertial;
using System.Collections.Generic;
using UnityEngine;

namespace Src.Move {

    public interface ICharacterMoveController {
        
    }
    
    public class CharacterMoveController : SerializedMonoBehaviour, ICharacterMoveController {

        [OdinSerialize]
        [LabelText("運動量管理クラス")]
        private List<IMovementProvider> m_movementManagers = new();

        private Vector3 m_currentMovement;

        private void FixedUpdate() {

            m_currentMovement = CalculateTotalMovement();

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