using System;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Sound {
    
    public interface ISoundManager {
        
    }

    public class SoundManager : ISoundManager, IStartable, IDisposable{

        [SerializeField]
        [ReadOnly]
        private AudioSource m_audioSource;
        
        private ISubscriber<IEmitSoundEventBus> m_subscriber;
        
        private IDisposable m_subscription;

        private IObjectResolver m_resolver;

        public SoundManager(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        public void Start() {
            m_audioSource = m_resolver.Resolve<AudioSource>();

            m_subscriber = m_resolver.Resolve<ISubscriber<IEmitSoundEventBus>>();

            m_subscription = m_subscriber.Subscribe(OnTakeEventBus);
        }

        public void Dispose() {
            m_subscription?.Dispose();
        }

        private void OnTakeEventBus(IEmitSoundEventBus eventBus) {

            if (eventBus is null || eventBus.Clip is null) return; 
            
            m_audioSource.PlayOneShot(eventBus.Clip);
            
        }
    }
}