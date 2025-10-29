using System;
using System.Linq;
using Sirenix.OdinInspector;
using Src.Bullet.Collision;
using UnityEngine;

namespace Src.Bullet.Context {
    [Serializable]
    public class IncreaseDamage : ABulletContext {

        [SerializeField]
        [LabelText("増加量")]
        private int m_increaseValue = 10;

        public override void Apply(GameObject obj) {
            
            Debug.Log("IncreaseDamage");
            
            var onCollision = GetComponentsFromBullet<IDamageOnCollision>(obj);

            if (onCollision is null) {
                Debug.Log("OnCollision is null");
                return;
            }
            
            onCollision.ToList().ForEach(x => x.AddDamage(m_increaseValue));
        }
    }
}