using UnityEngine;

namespace Src.Target {
    public interface ITargetManager {
        
        void ChangeTarget(GameObject target);
        
        void DisTarget();
    }
}