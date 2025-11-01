using System;
using System.Collections.Generic;
using Src.Enemy;
using UnityEngine;
using VContainer;

namespace Src.Bot {
    [Serializable]
    public class AttackState : IEnemyState {
        
        private Animator m_animator;

        private IEnemyStateManager m_state;
        
        private IAttackCollisionManager m_collisionManager;
        
        public void Initialize(GameObject obj, IObjectResolver resolver) {
            
            m_state = resolver.Resolve<IEnemyStateManager>();

            m_collisionManager = resolver.Resolve<IAttackCollisionManager>();

        }

        public void Start() {
            m_animator.Play("Attack");
            m_collisionManager.Activate();
        }

        public void Update() {
            AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime > 1.0f) {
                m_state.SetState(new IdleState());
            }
        }

        public void Exit() {
            m_collisionManager.Deactivate();
        }
    }
}