using RinaBullet.Collision;
using Src.Bullet.Collision;
using UnityEngine;

namespace Src.Bullet.Context {
    public class GiveInstancePrefabOnCollision : ABulletContext {

        public override void Apply(GameObject obj) {
            
            var collisionManager = GetComponentsFromBullet<ICollisionCallBackManager>(obj);

            var element = obj.AddComponent<InstancePrefabOnCollision>();
        }
    }
}