using GeneralModule.Scope;
using MessagePipe;
using Sirenix.OdinInspector;
using Src.Camera;
using Src.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.GameManager {
    public class GameManagerLifetimeScope : ListableLifetimeScope {
        
        [SerializeField]
        [LabelText("プレイヤー")]
        private Player.Player m_player;

        [SerializeField]
        [LabelText("カメラ")]
        private UnityEngine.Camera m_camera;

        protected override void Configure(IContainerBuilder builder) {
            
            base.Configure(builder);

            builder
                .RegisterComponent(m_player)
                .As<Player.Player>()
                .As<IPlayer>();

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