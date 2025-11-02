using Sirenix.OdinInspector;
using Src.Target;
using Src.Utility;
using Unity.VisualScripting;
using VContainer;
using VContainer.Unity;

namespace Src.Bot {
    public class EnemyBotInstaller : SerializedMonoBehaviour, IInstaller {

        public void Install(IContainerBuilder builder) {
            var stateManager = gameObject.GetComponentFromWhole<IEnemyStateManager>();

            if (stateManager is not null) {
                builder
                    .RegisterComponent(stateManager)
                    .As<IEnemyStateManager>();
            }

            builder
                .Register<EnemyTargetProvider>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}