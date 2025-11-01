using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Src.Enemy {

    public interface IAttackCollisionManager {

        void Activate();
        
        void Deactivate();
        
    }
    
    public class AttackCollisionManager : SerializedMonoBehaviour {

        private List<GameObject> m_collisions = new ();
        
        private List<Collider> m_colliders = new ();
        
    }
}