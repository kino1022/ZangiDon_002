using System;
using System.Runtime.Serialization;
using UnityEngine;
using VContainer;

namespace Src.Bot {
    [Serializable]
    public class IdleState : IEnemyState {
        
        private IEnemyStateManager m_stateManager;
        
        private GameObject m_enemy;
        
        private Player.Player m_player;
        
        private IObjectResolver m_resolver;

        public void Initialize(GameObject obj, IObjectResolver resolver) {
            m_enemy = obj;
            
            m_resolver = resolver;
        }

        public void Start() {
            m_player = m_resolver.Resolve<Player.Player>();

            m_stateManager = m_resolver.Resolve<IEnemyStateManager>();
        }

        public void Update() {
            
        }

        public void Exit() {
            
        }
    }
}