using System.Collections.Generic;
using Sirenix.OdinInspector;
using Src.Utility;
using UnityEngine;

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


        private void Start() {
            
            m_colliders = GetColliders();

            if (m_colliders.Count is not 0 && m_colliders is not null) {
                Deactivate();
            }

        }

        public void Activate () {
            m_colliders.ForEach(x => x.enabled = true);
        }

        public void Deactivate () {
            m_colliders.ForEach (x => x.enabled = false);
        }

        private List<Collider> GetColliders() {

            var result = new List<Collider>();

            m_collisions
                .ForEach(c => {
                    var col = c.GetComponents<Collider>();

                    if (col.Length is 0 || col is null) {
                        return;
                    }

                    result.AddRange(col);
                });

            return result;
        }

    }
}