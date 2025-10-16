using Sirenix.OdinInspector;
using Src.Target;
using VContainer;
using VContainer.Unity;

namespace Src.GameManager.Entities.Installer {
    public class EntitiesModuleInstaller : SerializedMonoBehaviour, IInstaller {

        public void Install(IContainerBuilder builder) {
            
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