using System;
using MessagePipe;
using RinaBullet.Collision;
using RinaCorrection;
using Sirenix.OdinInspector;
using Src.Health;
using Src.Health.EventBus;
using Src.Utility;
using UnityEngine;
using VContainer;

namespace Src.Bullet.Collision {
    public class GiveDamageOnCollision : SerializedMonoBehaviour, ICollisionCallBackElement {

        [SerializeField]
        [LabelText("ダメージ量")]
        [ProgressBar(0,500)]
        private int m_damageValue = 10;

        [SerializeField]
        [LabelText("処理の優先度")]
        [ProgressBar(0, 10)]
        private int m_priority = 1;
        
        public int Priority => m_priority;
        
        private ICorrectionManager m_correctionManager;
        
        private IPublisher<ITakeDamageEventBus> m_publisher;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            
            m_correctionManager = m_resolver.Resolve<ICorrectionManager>() ?? throw new NullReferenceException();
            m_publisher = m_resolver.Resolve<IPublisher<ITakeDamageEventBus>>() ?? throw new NullReferenceException();
            
            var collisionManager = ComponentsUtility.GetComponentsFromWhole<ICollisionCallBackManager>(gameObject) ?? throw new NullReferenceException();
            
            collisionManager.AddOnCollision(this);
            
        }
        
        public void OnCollisionEnterCallBack(UnityEngine.Collision other) {
            
            var health = ComponentsUtility.GetComponentsFromWhole<IHealth>(other.gameObject);

            if (health == null) {
                Debug.Log("衝突したオブジェクトに体力の定義がありませんでした");
                return;
            }
            
            var root = other.transform.root.gameObject;
            
            Debug.Log($"体力の定義があるオブジェクト{root.gameObject.name}に衝突したためダメージの発行を行います");

            var damage = new Damage((int)m_correctionManager.Apply(m_damageValue));

            m_publisher.Publish(new TakeDamageEventBus(root, damage));
            
        }
    }
}