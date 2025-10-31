using System;
using MessagePipe;
using Src.Wave.EventBus;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.WavePopup {

    public interface IWavePopupProvider {
        
    }
    
    public class WavePopupProvider : IWavePopupProvider, IStartable, IDisposable {
        
        private IWavePopupFactory m_factory;
        
        private ISubscriber<IWaveStartEventBus> m_subscriber;
        
        private IDisposable m_subscription;
        
        private IObjectResolver m_resolver;

        public WavePopupProvider(IObjectResolver resolver) {
            m_resolver = resolver;
        }
        
        public void Start () {
            
            m_subscriber = m_resolver.Resolve<ISubscriber<IWaveStartEventBus>>();
            
            m_factory = m_resolver.Resolve<IWavePopupFactory>();

            m_subscription = m_subscriber.Subscribe(OnTakeEventBus);
        }

        public void Dispose() {
            m_subscription?.Dispose();
        }

        private void OnTakeEventBus(IWaveStartEventBus eventBus) {
            m_factory.Create(eventBus.WaveCount);
        }
    }
}