using GeneralModule.Scope;
using MessagePipe;
using Sirenix.OdinInspector;
using Src.Camera;
using UnityEngine;
using VContainer;

namespace Src.GameManager {
    public class GameManagerLifetimeScope : ListableLifetimeScope {

        [SerializeField]
        [LabelText("カメラ")]
        private UnityEngine.Camera m_camera;

        protected override void Configure(IContainerBuilder builder) {
            
            base.Configure(builder);

            builder
                .RegisterMessagePipe();
            
            builder
                .RegisterInstance(m_camera)
                .As<UnityEngine.Camera>();

            builder
                .Register<CameraDirectionProvider>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}