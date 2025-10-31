using Sirenix.OdinInspector;
using Src.Health.EventBus;
using Src.Utility;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Damage {
    public class DamagePopupInstaller : SerializedMonoBehaviour, IInstaller{

        public void Install(IContainerBuilder builder) {

            var factory = ComponentsUtility.GetComponentFromWhole<IDamagePopupFactory>(gameObject);
            
            builder
                .RegisterComponent(factory)
                .As<IDamagePopupFactory>();
            
            var provider = ComponentsUtility.GetComponentFromWhole<IDamagePopupProvider>(gameObject);
            
            builder
                .RegisterComponent(provider)
                .As<IDamagePopupProvider>();
            
        }
    }
}