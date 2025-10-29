using System;
using RinaBullet.Collision;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Bullet.Collision {
    public class InstancePrefabOnCollision : ACollisionElementBehaviour {
        
        [SerializeField]
        [LabelText("生成プレファブ")]
        private GameObject m_prefab;

        private void Start() {
            
        }
 
        public override void OnCollisionEnterCallBack(UnityEngine.Collision other) {
            m_resolver.Instantiate(
                m_prefab,
                other.contacts[0].normal,
                Quaternion.identity
            );
        }

        public void SetPrefab (GameObject obj) {
            if (obj is null) {
                return;
            }

            m_prefab = obj;
        }
    }
}