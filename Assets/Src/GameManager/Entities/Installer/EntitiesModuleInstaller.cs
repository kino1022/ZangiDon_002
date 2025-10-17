using Sirenix.OdinInspector;
using Src.Target;
using VContainer;
using VContainer.Unity;

namespace Src.GameManager.Entities.Installer {
    public class EntitiesModuleInstaller : SerializedMonoBehaviour, IInstaller {

        public void Install(IContainerBuilder builder) {
            
            var manager = gameObject.transform.root
                .GetComponentInChildren<IEntitiesManager>();

            builder
                .RegisterComponent(manager)
                .As<IEntitiesManager>();
            
            var provider = gameObject.transform.root
                .GetComponentInChildren<IEntitiesProvider>();
            
            builder
                .RegisterComponent(provider)
                .As<IEntitiesProvider>();
        }
    }
}