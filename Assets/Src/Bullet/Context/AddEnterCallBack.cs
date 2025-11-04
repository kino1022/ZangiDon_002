using System;
using System.Collections.Generic;
using RinaBullet.Collision;
using RinaBullet.Context;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Utility;

namespace Src.Bullet.Context {
    [Serializable]
    public class AddEnterCallBack : IBulletContext {
        
        [OdinSerialize]
        [LabelText("追加するコールバック")]
        private List<ICollisionCallBackElement> m_elements = new List<ICollisionCallBackElement>();

        public void Apply(UnityEngine.GameObject obj) {
            
            if (m_elements.Count is 0 || m_elements is null) {
                return;
            }
            
            var manager = obj.GetComponentFromWhole<ICollisionCallBackManager>();

            if (manager is null) {
                return;
            }

            foreach (var element in m_elements) {
                manager.AddOnCollision(element);
            }
            
        }
    }
}