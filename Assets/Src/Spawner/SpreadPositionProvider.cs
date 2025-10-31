using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Src.Spawner {
    [Serializable]
    public class SpreadPositionProvider : ISpawnPositionProvider {

        [SerializeField] 
        [LabelText("範囲")] 
        private float m_range = 20.0f;

        public Vector3 Provide(GameObject spawner, IObjectResolver resolver) {
            
            var result = spawner.transform.position;
            
            result.x += Random.Range(-m_range, m_range);
            
            result.z += Random.Range(-m_range, m_range);
            
            return result;
        }
    }
}