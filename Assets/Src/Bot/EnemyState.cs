using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Src.Bot {

    public interface IEnemyState {
        
        void Initialize(GameObject obj, IObjectResolver resolver);

        void Start();
        
        void Update();

        void Exit();
        
    }

    [Serializable]
    public class WalkState : IEnemyState {

        private GameObject m_enemy;
        
        private Player.Player m_player;

        private float m_attackRange = 2.0f;
        
        private IEnemyStateManager m_stateManager;
        
        private NavMeshAgent m_agent;

        private IObjectResolver m_resolver;

        public void Initialize(GameObject obj, IObjectResolver resolver) {
            
            m_enemy = obj;
            
            m_resolver = resolver;

            m_stateManager = m_resolver.Resolve<IEnemyStateManager>();
            
            m_agent = m_resolver.Resolve<NavMeshAgent>();
            
        }

        public void Start() {
            
            m_agent.isStopped = false;
            
        }

        public void Update() {

            //距離が一定以内なら攻撃に移行する
            if ((m_player.transform.position - m_enemy.transform.position).magnitude < m_attackRange) {
                m_stateManager.SetState(new AttackState());
            }
            
        }

        public void Exit() {
            
        }
        
    }
    [Serializable]
    public class IdleState : IEnemyState {
        public void Initialize(GameObject obj, IObjectResolver resolver) {
            
        }

        public void Start() {
            
        }

        public void Update() {
            
        }

        public void Exit() {
            
        }
    }

    public class AttackState : IEnemyState {

        public void Initialize(GameObject obj, IObjectResolver resolver) {
            
        }

        public void Start() {
            
        }

        public void Update() {
            
        }

        public void Exit() {
            
        }
        
    }
}