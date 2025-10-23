using UnityEngine;

namespace Src.Move {
    
    public interface IMovementManager {
        
        /// <summary>
        /// 運動量
        /// </summary>
        Vector3 Movement { get; }
        
    }
    
}