using System;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Src.Bot {

    public interface IEnemyStateManager {
        
        ReadOnlyReactiveProperty<IEnemyState> CurrentState { get; }

        void SetState(IEnemyState state);
        
    }
    
    public class EnemyStateManager : SerializedMonoBehaviour, IEnemyStateManager {

        private ReactiveProperty<IEnemyState> m_currentState;
        
        private IObjectResolver m_resolver;
        
        public ReadOnlyReactiveProperty<IEnemyState> CurrentState => m_currentState;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        public void SetState(IEnemyState state) {
            
            m_currentState.CurrentValue.Exit();
            
            state.Initialize(gameObject, m_resolver);
            
            m_currentState.Value = state;
            
            m_currentState.CurrentValue.Start();
        }

        private void Update() {
            m_currentState.Value.Update();
        }
    }
    
}