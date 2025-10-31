using Sirenix.OdinInspector;
using Src.Utility;
using VContainer;
using VContainer.Unity;

namespace Src.Shoot {
    public class ShootSystemInstaller : SerializedMonoBehaviour, IInstaller {

        public void Install(IContainerBuilder builder) {
            
            var adjustor = ComponentsUtility.GetComponentFromWhole<IMuzzleAdjustor>(gameObject);

            builder
                .RegisterComponent(adjustor)
                .As<IMuzzleAdjustor>();
            
        }
    }
}