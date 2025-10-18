using Sirenix.OdinInspector;
using Src.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.LockOn {
    public class LockOnMarkerInsatller : SerializedMonoBehaviour, IInstaller {

        [SerializeField]
        [LabelText("ÉJÉÅÉâ")]
        private UnityEngine.Camera m_camera;

        public void Install(IContainerBuilder builder) {

            builder.RegisterInstance(m_camera)
                .As<UnityEngine.Camera>();

            builder.Register<ILockOnMarkerPresnter, LockOnMarkerPresenter>(Lifetime.Singleton);

            var view = ComponentsUtility.GetComponentsFromWhole<ILockOnMarkerView>(gameObject);

            builder
                .RegisterComponent(view)
                .As<ILockOnMarkerView>();

            builder
                .RegisterInstance(m_camera)
                .As<UnityEngine.Camera>();

        }
    }
}