using System;
using GeneralModule.Symbol;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using VContainer;
using VContainer.Unity;
using Src.Player;
using UnityEngine;

namespace Src.Cycle {
    [Serializable]
    public class GameStart : IStartable {

        [OdinSerialize]
        [LabelText("プレイヤーのプレハブ")]
        private Player.Player m_playerPrefab;
        
        [SerializeField]
        [LabelText("スポーンする座標")]
        private Vector3 m_startPosition = Vector3.zero;
        
        private IObjectResolver m_resolver;

        [Inject]
        public GameStart(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public void Start() {
            m_resolver.Instantiate(
                prefab:m_playerPrefab,
                position:m_startPosition,
                rotation: new Quaternion(0,0,0,0)
            );
        }
    }
}