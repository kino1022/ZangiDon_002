using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Src.Move {

    public interface IFreeFallManager : IMovementManager {
        
        public void SetEnabled(bool enabled);
        
    }
    
    [Serializable]
    public class FreeFallManager : SerializedMonoBehaviour, IFreeFallManager {
        
        [SerializeField]
        [LabelText("自由落下が有効か")]
        private bool m_freeFallEnable = true;

        public Vector3 Movement => m_freeFallEnable ? Physics.gravity : Vector3.zero;

        public void SetEnabled(bool enabled) {
            m_freeFallEnable = enabled;
        }
        
    }
}