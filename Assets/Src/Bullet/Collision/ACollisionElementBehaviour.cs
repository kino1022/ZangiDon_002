using RinaBullet.Collision;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Utility;
using UnityEngine;
using VContainer;

namespace Src.Bullet.Collision {

    internal enum Timing {
        OnEnter,
        OnExit,
    }
    
    public abstract class ACollisionElementBehaviour : SerializedMonoBehaviour, ICollisionCallBackElement {

        [SerializeField] 
        [LabelText("処理の優先度")] 
        private int m_priority = 0;
        
        [OdinSerialize]
        [LabelText("発動タイミング")]
        private Timing m_timing = Timing.OnEnter;
        
        protected IObjectResolver m_resolver;

        public int Priority => m_priority;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }
        
        private void Start() {
            
            RegisterCollisionManager();
            
            ResolveDependence();
        }

        public abstract void OnCollisionEnterCallBack(UnityEngine.Collision other);

        protected virtual void ResolveDependence () {}

        private void RegisterCollisionManager() {
            
            var mana = ComponentsUtility.GetComponentFromWhole<ICollisionCallBackManager>(gameObject);

            if (mana is null) {
                enabled = false;
                return;
            }

            switch (m_timing) {
                case Timing.OnEnter:
                    mana.AddOnCollision(this);
                    break;
                case Timing.OnExit:
                    mana.RemoveOnCollision(this);
                    break;
            }
            
        }
    }
}