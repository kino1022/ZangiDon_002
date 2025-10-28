using System;
using RinaBullet.Context;
using UnityEngine;

namespace Src.Bullet.Context {
    [Serializable]
    public abstract class ABulletContext : IBulletContext {
        
        public abstract void Apply(GameObject obj);
        
        protected T[] Apply_Implement<T>(GameObject obj) {
            return obj.transform.root.GetComponentsInChildren<T>(true);
        }
        
    }
}