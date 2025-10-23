using GeneralModule.Scope;
using MessagePipe;
using Src.Spell.EventBus.Interface;
using Src.Utility;
using VContainer;
using VContainer.Unity;

namespace Src.Player {
    public class PlayerLifetimeScope : ListableLifetimeScope {

        protected override void Configure(IContainerBuilder builder) {
            base.Configure(builder);

            var symbol = ComponentsUtility.GetComponentsFromWhole<IPlayer>(gameObject);

            builder
                .RegisterComponent(symbol)
                .As<IPlayer>();
        }
    }
}