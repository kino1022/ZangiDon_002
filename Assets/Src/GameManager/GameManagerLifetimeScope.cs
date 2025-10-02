using GeneralModule.Scope;
using MessagePipe;
using VContainer;
using MessagePipe.VContainer;
using VContainer.Unity;

namespace Src.GameManager {
    public class GameManagerLifetimeScope : ListableLifetimeScope {

        protected override void Configure(IContainerBuilder builder) {
            base.Configure(builder);

            builder.RegisterMessagePipe();
        }
    }
}