using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Utility;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.WavePopup {
    public class WavePopUpInstaller : SerializedMonoBehaviour, IInstaller {
        
        [OdinSerialize]
        private IWavePopupProvider m_provider;
        
        private IObjectResolver m_resolver;
        
        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_provider = m_resolver.Resolve<IWavePopupProvider>();
        }

        public void Install(IContainerBuilder builder) {
            
            builder
                .Register<WavePopupProvider>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            
            var factory = gameObject.GetComponentFromWhole<IWavePopupFactory>();

            if (factory is not null) {
                builder
                    .RegisterComponent(factory)
                    .As<IWavePopupFactory>();
            }
        }
    }
}