using Sirenix.OdinInspector;
using VContainer;
using VContainer.Unity;

namespace Src.Target.Installer {
    public class TargetModuleInstaller : SerializedMonoBehaviour, IInstaller {

        public void Install(IContainerBuilder builder) {

            builder
                .Register<IDirectionProvider, DirectionProvider>(Lifetime.Singleton);
            
            builder
                .Register<IDistanceProvider, DistanceProvider>(Lifetime.Singleton);
            
            var manager = gameObject.transform.root
                .GetComponentInChildren<ITargetManager>();

            builder
                .RegisterComponent(manager)
                .As<ITargetManager>();
            
            var provider = gameObject.transform.root
                .GetComponentInChildren<ITargetProvider>();
            
            builder
                .RegisterComponent(provider)
                .As<ITargetProvider>();
        }
    }
}