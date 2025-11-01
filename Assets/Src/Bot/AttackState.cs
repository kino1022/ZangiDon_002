using System;
using System.Collections.Generic;
using Src.Enemy;
using UnityEngine;
using VContainer;

namespace Src.Bot {
    [Serializable]
    public class AttackState : IEnemyState {
        
        private Animator m_animator;
        
        private IAttackCollisionManager m_collisionManager;
        
        public void Initialize(GameObject obj, IObjectResolver resolver) {
            
        }

        public void Start() {
            m_animator.Play("Attack");
        }

        public void Update() {
            
        }

        public void Exit() {
            
        }
    }
}