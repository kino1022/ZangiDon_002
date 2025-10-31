using System;
using MessagePipe;
using Src.Wave.EventBus;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Wave {

    public interface IWaveIndicationPresenter {
        
    }
    
    [Serializable]
    public class WaveIndicationPresenter : IWaveIndicationPresenter, IDisposable, IStartable {
        
        private ISubscriber<IWaveStartEventBus> m_subscriber;
        
        private IDisposable m_subscription;

        private IWaveIndicationView m_view;

        public WaveIndicationPresenter(ISubscriber<IWaveStartEventBus> subscriber, IWaveIndicationView view) {
            
            m_subscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));
            
            m_view = view ?? throw new ArgumentNullException(nameof(view));
            
        }

        public void Start() {
            m_subscription = m_subscriber.Subscribe(OnTakeEventBus);
        }

        public void Dispose() {
            m_subscription?.Dispose();
        }

        private void OnTakeEventBus(IWaveStartEventBus eventBus) {
            m_view.UpdateWave(eventBus.WaveCount);
        }
    }
    
}