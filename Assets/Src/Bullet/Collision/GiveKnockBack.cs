using System;
using RinaBullet.Collision;
using Sirenix.OdinInspector;
using Src.Move.Inertial;
using Src.Utility;
using UnityEngine;

namespace Src.Bullet.Collision {
    /// <summary>
    /// 指定した方向に対してノックバックを与える
    /// </summary>
    [Serializable]
    public class GiveKnockBack : ICollisionCallBackElement {
        
        [SerializeField]
        [LabelText("ノックバックの強さ")]
        private float m_force = 10.0f;

        [SerializeField]
        [LabelText("ノックバックの方向")]
        private Vector3 m_direction;
        
        [SerializeField]
        [LabelText("減衰率")]
        private float m_damping = 0.9f;

        [SerializeField]
        [LabelText("")]
        private int m_priority = 4;
        
        public int Priority => m_priority;

        public void OnCollisionEnterCallBack(UnityEngine.Collision other) {
            
            //慣性制御マネージャの取得処理
            var inertialManager = other.gameObject.GetComponentFromContainer<InertialManager>();
            if (inertialManager is null) {
                inertialManager = other.gameObject.GetComponentFromWhole<InertialManager>();
                if (inertialManager is null) {
                    return;
                }
            }

            //ワールド座標系に変換した方向ベクトルを計算
            var worldDir = other.transform != null
                ? other.transform.TransformDirection(m_direction).normalized
                : m_direction.normalized;

            //慣性の生成処理
            var inertial = new Inertial(worldDir, m_force, m_damping);
            
            inertialManager.Add(inertial);
            
        }
    }
}