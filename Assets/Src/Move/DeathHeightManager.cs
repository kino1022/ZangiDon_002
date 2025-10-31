using MessagePipe;
using R3;
using Sirenix.OdinInspector;
using Src.Health.EventBus;
using UnityEngine;
using VContainer;

namespace Src.Move {

    public interface IDeathHeightManager {
        
    }
    
    public class DeathHeightManager : SerializedMonoBehaviour {
        
        [Title("設定")]

        [SerializeField]
        [LabelText("死亡高度")]
        private float m_deathHeight = -320.0f;

        [Title("参照")] 
        
        [SerializeField]
        [LabelText("監視対象")]
        private Transform m_transform;
        
        private IPublisher<IOnDeadEventBus> m_publisher;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
           m_resolver = resolver; 
        }

        private void Start() {
            
            m_publisher = m_resolver.Resolve<IPublisher<IOnDeadEventBus>>();
            
            CreateStream();
            
        }

        private void CreateStream() {

            Observable
                .EveryUpdate()
                .Subscribe(_ => {
                    
                    var currentHeight = m_transform.position.y;

                    if (currentHeight < m_deathHeight) {
                        m_publisher.Publish(new OnDeadEventBus(transform.root.gameObject));
                    }
                })
                .AddTo(this);
        }
    }
}