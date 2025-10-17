using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Src.Spawner {
    [Serializable]
    public class RoundPositionProvider : ISpawnPositionProvider {

        [SerializeField]
        [LabelText("半径")]
        private float m_radius = 10.0f;
        
        public Vector3 Provide(GameObject spawner, IObjectResolver resolver) {
            Vector3 pointOnUnitSphere = Random.onUnitSphere;

            // 2. Y座標を絶対値にすることで、常に上半球（Yが正）の点を指すようにする
            pointOnUnitSphere.y = Mathf.Abs(pointOnUnitSphere.y);

            // 3. 中心の座標と半径を適用して最終的な位置を計算する
            return spawner.gameObject.transform.position + pointOnUnitSphere * m_radius;
        }
    }
}