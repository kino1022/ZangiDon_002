using System.Collections.Generic;
using Sirenix.OdinInspector;
using Src.Utility;
using UnityEngine;
using MessagePipe;
using Src.Health.EventBus;
using Src.Health;
using VContainer;

namespace Src.Enemy {

    public interface IAttackCollisionManager {

        void Activate();
        
        void Deactivate();
        
    }
    
    public class AttackCollisionManager : SerializedMonoBehaviour, IAttackCollisionManager {
        [Title("設定")]

        [SerializeField]
        [LabelText("当たり判定を有するオブジェクト")]
        private List<GameObject> m_collisions = new ();

        [Title("ランタイム")]

        [SerializeField]
        [LabelText("検知した当たり判定")]
        [ReadOnly]
        private List<Collider> m_colliders = new ();

        // 追加: ダメージ量（デバッグ用）
        [SerializeField]
        [LabelText("ダメージ量（即時Overlap発火用）")]
        [ProgressBar(0, 500)]
        private int m_damageValue = 10;

        // DI 用 resolver と publisher
        private IObjectResolver m_resolver;
        private IPublisher<ITakeDamageEventBus> m_damagePublisher;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            // Construct が呼ばれる前に Start が走る可能性があるため、resolver が null の場合は ComponentsUtility から取得を試みる
            if (m_resolver == null) {
                try {
                    m_resolver = gameObject.GetComponentFromContainer<IObjectResolver>();
                }
                catch {
                    m_resolver = null;
                }
            }

            if (m_resolver != null) {
                try {
                    m_damagePublisher = m_resolver.Resolve<IPublisher<ITakeDamageEventBus>>();
                }
                catch {
                    m_damagePublisher = null;
                }
            }

            m_colliders = GetColliders();

            // 安全な null/空チェック（GetColliders は null を返さない想定だが念のため）
            if (m_colliders != null && m_colliders.Count > 0) {
                // 初期は当たり判定を無効化しておく
                Deactivate();
                Debug.Log($"[AttackCollisionManager] Found {m_colliders.Count} colliders and deactivated them.", this);
            } else {
                Debug.Log("[AttackCollisionManager] No colliders found to manage.", this);
            }

        }

        public void Activate () {
            if (m_colliders == null || m_colliders.Count == 0) {
                Debug.LogWarning("[AttackCollisionManager] Activate called but no colliders to enable.", this);
                return;
            }
            m_colliders.ForEach(x => {
                if (x != null) x.enabled = true;
            });
            Debug.Log($"[AttackCollisionManager] Activated {m_colliders.Count} colliders.", this);

            // 追加: CharacterController を使っている場合など OnCollision が来ないことがあるため、
            // 有効化直後に重なり判定を取って当たり判定を即時発火させる
            PerformImmediateOverlapChecks();
        }

        public void Deactivate () {
            if (m_colliders == null || m_colliders.Count == 0) return;
            m_colliders.ForEach (x => { if (x != null) x.enabled = false; });
            Debug.Log($"[AttackCollisionManager] Deactivated {m_colliders.Count} colliders.", this);
        }

        private List<Collider> GetColliders() {

            var result = new List<Collider>();

            // 子オブジェクトも含めてコライダーを取得する
            m_collisions
                .ForEach(c => {
                    if (c == null) return;

                    var cols = c.GetComponentsInChildren<Collider>(includeInactive: true);

                    if (cols == null || cols.Length == 0) {
                        return;
                    }

                    result.AddRange(cols);
                });

            return result;
        }

        // 追加: 即時重なりチェック（Box/Sphere/Capsule に対応）
        private void PerformImmediateOverlapChecks() {

            if (m_colliders == null || m_colliders.Count == 0) return;

            foreach (var col in m_colliders) {
                if (col == null) continue;

                Collider[] hits = null;

                if (col is BoxCollider box) {
                    var center = box.transform.TransformPoint(box.center);
                    var halfExtents = Vector3.Scale(box.size * 0.5f, box.transform.lossyScale);
                    hits = Physics.OverlapBox(center, halfExtents, box.transform.rotation, ~0, QueryTriggerInteraction.Collide);
                }
                else if (col is SphereCollider sph) {
                    var center = sph.transform.TransformPoint(sph.center);
                    var radius = sph.radius * Mathf.Max(sph.transform.lossyScale.x, Mathf.Max(sph.transform.lossyScale.y, sph.transform.lossyScale.z));
                    hits = Physics.OverlapSphere(center, radius, ~0, QueryTriggerInteraction.Collide);
                }
                else if (col is CapsuleCollider cap) {
                    var center = cap.transform.TransformPoint(cap.center);
                    var dir = Vector3.up;
                    switch (cap.direction) {
                        case 0: dir = cap.transform.right; break; // X
                        case 1: dir = cap.transform.up; break;    // Y
                        case 2: dir = cap.transform.forward; break; // Z
                    }

                    float height = Mathf.Max(0, cap.height * cap.transform.lossyScale.y);
                    float radius = cap.radius * Mathf.Max(cap.transform.lossyScale.x, cap.transform.lossyScale.z);

                    Vector3 pointA = center + dir * (height * 0.5f - radius);
                    Vector3 pointB = center - dir * (height * 0.5f - radius);

                    hits = Physics.OverlapCapsule(pointA, pointB, radius, ~0, QueryTriggerInteraction.Collide);
                }
                else {
                    // その他の Collider は Bounds を使って OverlapBox で試す
                    var b = col.bounds;
                    hits = Physics.OverlapBox(b.center, b.extents, Quaternion.identity, ~0, QueryTriggerInteraction.Collide);
                }

                if (hits == null || hits.Length == 0) continue;

                Debug.Log($"[AttackCollisionManager] Immediate overlap detected {hits.Length} hits for collider {col.name}", this);

                foreach (var hit in hits) {
                    if (hit == null) continue;

                    // 重なっているオブジェクトの root に IHealth があるか確認
                    var root = hit.transform.root.gameObject;

                    var health = ComponentsUtility.GetComponentFromWhole<IHealth>(root);

                    if (health != null) {
                        Debug.Log($"[AttackCollisionManager] Found IHealth on {root.name} -> publishing damage {m_damageValue}", this);

                        if (m_damagePublisher != null) {
                            var dmg = new Src.Health.Damage(m_damageValue);
                            m_damagePublisher.Publish(new Src.Health.EventBus.TakeDamageEventBus(root, dmg));
                        }
                    }
                    else {
                        Debug.Log($"[AttackCollisionManager] Overlapped object {root.name} has no IHealth.", this);
                    }
                }

            }
        }

    }
}