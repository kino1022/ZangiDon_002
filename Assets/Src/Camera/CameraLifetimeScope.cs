using GeneralModule.Scope;
using VContainer;

namespace Src.Camera {
    public class CameraLifetimeScope : ListableLifetimeScope {
        protected override void Configure(IContainerBuilder builder) {
            base.Configure(builder);
            
            var modeManager = gameObject.transform.root
                .GetComponentInChildren<ICameraModeManager>();
            
            builder
                .RegisterInstance(modeManager)
                .As<ICameraModeManager>();
        }
    }
}