using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Utility;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Wave {
    public class WaveIndicationInstaller : SerializedMonoBehaviour, IInstaller {
        
        [OdinSerialize]
        private IWaveIndicationPresenter m_presenter;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_presenter = m_resolver.Resolve<IWaveIndicationPresenter>();
        }

        public void Install(IContainerBuilder builder) {
            
            builder
                .Register<IWaveIndicationPresenter, WaveIndicationPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces();

            var view = gameObject.GetComponentFromWhole<IWaveIndicationView>();

            if (view is not null) {
                builder
                    .RegisterComponent(view)
                    .As<IWaveIndicationView>();
            } 

        }
    }
}