using System;
using MessagePipe;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.GameOver;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.GameOver {

    public interface IGameOverPopupProvider {
        
    }
    
    public class GameOverPopupProvider : IGameOverPopupProvider, IStartable, IDisposable {
        [Title("参照")]
        
        [OdinSerialize]
        [LabelText("ポップアップ生成")]
        [ReadOnly]
        private IGameOverPopupFactory m_factory;
        
        private ISubscriber<IGameOverEventBus> m_subscriber;
        
        private IDisposable m_subscription;
        
        private IObjectResolver m_resolver;

        public GameOverPopupProvider(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        public void Start() {
            
            m_factory = m_resolver.Resolve<IGameOverPopupFactory>();
            
            m_subscriber = m_resolver.Resolve<ISubscriber<IGameOverEventBus>>();

            m_subscription = m_subscriber.Subscribe(OnTakeEventBus);
        }

        public void Dispose() {
            m_subscription?.Dispose();
        }

        private void OnTakeEventBus(IGameOverEventBus eventBus) {
            m_factory.Create(eventBus.FinalWave);
        }
    }
    
}