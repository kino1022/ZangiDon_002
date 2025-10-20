using Sirenix.OdinInspector;
using Src.Health.EventBus;
using Src.Utility;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Damage {
    public class DamagePopupInstaller : SerializedMonoBehaviour, IInstaller{

        public void Install(IContainerBuilder builder) {

            var factory = ComponentsUtility.GetComponentsFromWhole<IDamagePopupFactory>(gameObject);
            
            builder
                .RegisterComponent(factory)
                .As<IDamagePopupFactory>();
            
            var provider = ComponentsUtility.GetComponentsFromWhole<IDamagePopupProvider>(gameObject);
            
            builder
                .RegisterComponent(provider)
                .As<IDamagePopupProvider>();
            
        }
    }
}