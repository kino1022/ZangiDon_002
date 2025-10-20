using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.LockOn {
    public class LockOnMarkerInstaller : SerializedMonoBehaviour, IInstaller {

        [SerializeField]
        [LabelText("カメラ")]
        private UnityEngine.Camera m_camera;

        [OdinSerialize]
        [LabelText("プレゼンター")]
        private ILockOnMarkerPresenter m_presenter;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_presenter = m_resolver.Resolve<ILockOnMarkerPresenter>();
            m_presenter.Start();
        }

        public void Install(IContainerBuilder builder) {

            builder.RegisterInstance(m_camera)
                .As<UnityEngine.Camera>();

            builder.Register<ILockOnMarkerPresenter, LockOnMarkerPresenter>(Lifetime.Singleton);

            var view = ComponentsUtility.GetComponentsFromWhole<ILockOnMarkerView>(gameObject);

            builder
                .RegisterComponent(view)
                .As<ILockOnMarkerView>();

        }
    }
}