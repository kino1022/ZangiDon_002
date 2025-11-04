using System;
using RinaBullet.Collision;
using RinaCorrection.Asset;
using RinaCorrection.Definition;
using Sirenix.OdinInspector;
using Src.Enemy;
using Src.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Bullet.Collision {
    
    internal enum CorrectionType {
        Fixed,
        Ratio,
    }
    
    [Serializable]
    public class DecreaseSpeed : ICollisionCallBackElement {
        
        [SerializeField]
        [LabelText("補正タイプ")]
        private CorrectionType m_type = CorrectionType.Fixed;

        [SerializeField] 
        [LabelText("減少量")]
        private float m_value = -0.4f;

        [SerializeField]
        [LabelText("持続時間")]
        private float m_duration = 4.0f;

        [SerializeField]
        [LabelText("処理優先度")]
        private int m_priority = 3;
        
        public int Priority => m_priority;

        public void OnCollisionEnterCallBack(UnityEngine.Collision other) {

            if (other == null || other.gameObject == null) {
                Debug.LogWarning("OnCollisionEnterCallBack: other または other.gameObject が null です");
                return;
            }

            var go = other.gameObject;
            Debug.Log($"DecreaseSpeed: 衝突通知を受信しました -> {go.name}");

            // 1) まず通常の GetComponent を試す（最も速く確実）
            IEnemySpeed speed = null;
            try {
                speed = go.GetComponent<IEnemySpeed>();
                if (speed != null) {
                    Debug.Log($"IEnemySpeed を GetComponent で取得しました: {go.name}");
                }
            } catch (Exception ex) {
                Debug.LogWarning($"GetComponent<IEnemySpeed> で例外: {ex.Message}");
            }

            // 2) 次に拡張メソッド経由でコンテナから取得する実装があるなら試す（例外を捕捉）
            if (speed == null) {
                try {
                    speed = go.GetComponentFromContainer<IEnemySpeed>();
                    if (speed != null) Debug.Log($"IEnemySpeed をコンテナ経由で取得しました: {go.name}");
                } catch (Exception ex) {
                    Debug.LogWarning($"GetComponentFromContainer<IEnemySpeed> で例外: {ex.Message}");
                }
            }

            // 3) それでも null なら親の LifetimeScope を探してコンテナから Resolve を試みる
            LifetimeScope lifetimeScope = null;
            if (speed == null) {
                try {
                    lifetimeScope = go.GetComponentInParent<LifetimeScope>();
                    if (lifetimeScope != null) {
                        try {
                            speed = lifetimeScope.Container.Resolve<IEnemySpeed>();
                            Debug.Log($"IEnemySpeed を親の LifetimeScope コンテナから Resolve しました: {go.name}");
                        } catch (Exception ex) {
                            Debug.LogWarning($"LifetimeScope.Container.Resolve<IEnemySpeed> に失敗: {ex.Message}");
                        }
                    } else {
                        Debug.Log($"LifetimeScope が見つかりませんでした: {go.name}");
                    }
                } catch (Exception ex) {
                    Debug.LogWarning($"GetComponentInParent<LifetimeScope> で例外: {ex.Message}");
                }
            } else {
                // speed が既に取得できている場合でも、LifetimeScope を後で使う可能性があるため親を探す
                try { lifetimeScope = go.GetComponentInParent<LifetimeScope>(); } catch { lifetimeScope = null; }
            }

            if (speed == null) {
                Debug.Log($"衝突したオブジェクトに IEnemySpeed が見つかりませんでした: {go.name}");
                return;
            }

            // LifetimeScope を使って補正タイプを Resolve する
            if (lifetimeScope == null) {
                // まだ取得できていないならもう一度探す
                lifetimeScope = go.GetComponentInParent<LifetimeScope>();
            }

            if (lifetimeScope == null) {
                Debug.LogWarning($"補正タイプを Resolve するための LifetimeScope が見つかりません: {go.name}");
                return;
            }

            ICorrectionType type;
            try {
                type = m_type == CorrectionType.Fixed
                    ? lifetimeScope.Container.Resolve<FixedType>()
                    : lifetimeScope.Container.Resolve<RatioType>();
            } catch (Exception ex) {
                Debug.LogWarning($"補正タイプの Resolve に失敗しました: {ex.Message}");
                return;
            }

            var correction = new TimeLimitCorrection(type, m_value, TimeSpan.FromSeconds(m_duration));

            try {
                speed.Correction.Add(correction);
                Debug.Log($"速度補正を追加しました: {go.name} 値={m_value} 時間={m_duration}");
            } catch (Exception ex) {
                Debug.LogWarning($"speed.Correction.Add で例外: {ex.Message}");
            }

        }
    }
}