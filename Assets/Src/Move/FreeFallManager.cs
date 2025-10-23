using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Src.Move {

    public interface IFreeFallManager {
        
        public void SetEnabled(bool enabled);
        
    }
    
    [Serializable]
    public class FreeFallManager : IMovementManager {
        
        private bool m_freeFallEnable = true;

        public Vector3 Movement => m_freeFallEnable ? Physics.gravity : Vector3.zero;

        public void SetEnabled(bool enabled) {
            m_freeFallEnable = enabled;
        }
        
    }
}