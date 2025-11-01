using Src.Target;
using System;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Src.Bot {
    [Serializable]
    public class WalkState : IEnemyState {

        private GameObject m_enemy;

        private ITargetProvider m_target;

        private float m_attackRange = 2.0f;
        
        private IEnemyStateManager m_stateManager;
        
        private Animator m_animator;
        
        private NavMeshAgent m_agent;

        private IObjectResolver m_resolver;

        public void Initialize(GameObject obj, IObjectResolver resolver) {
            
            m_enemy = obj;
            
            m_resolver = resolver;

            m_target = resolver.Resolve<ITargetProvider>();

            m_stateManager = m_resolver.Resolve<IEnemyStateManager>();
            
            m_agent = m_resolver.Resolve<NavMeshAgent>();
            
            m_animator = m_resolver.Resolve<Animator>();
            
        }

        public void Start() {
            
            m_agent.isStopped = false;

            m_animator.Play("Walk");

        }

        public void Update() {

            //距離が一定以内なら攻撃に移行する
            if ((m_target.Target.CurrentValue.transform.position - m_enemy.transform.position).magnitude < m_attackRange) {
                m_stateManager.SetState(new AttackState());
            }
            
        }

        public void Exit() {
            
        }
        
    }
}