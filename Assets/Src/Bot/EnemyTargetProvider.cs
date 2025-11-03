using System;
using R3;
using Sirenix.OdinInspector;
using Src.Target;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Bot {
    [Serializable]
    public class EnemyTargetProvider :SerializedMonoBehaviour, ITargetProvider {
        
        private ReactiveProperty<GameObject> m_target;
        
        [Title("参照")]
        
        [SerializeField]
        [ReadOnly]
        private Player.Player m_player;
        
        private IObjectResolver m_resolver;
        
        public ReadOnlyReactiveProperty<GameObject> Target => m_target;
        
        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            
            m_player = m_resolver.Resolve<Player.Player>();
            
            m_target = new ReactiveProperty<GameObject>(m_player.gameObject);
            
            m_target.Value = m_player.gameObject;
            
        }
        
    }
}