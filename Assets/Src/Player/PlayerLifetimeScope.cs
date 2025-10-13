using GeneralModule.Scope;
using MessagePipe;
using Src.Spell.EventBus.Interface;
using VContainer;

namespace Src.Player {
    public class PlayerLifetimeScope : ListableLifetimeScope {
        protected override void Configure(IContainerBuilder builder) {
            base.Configure(builder);

            var options = builder.RegisterMessagePipe();

            builder.RegisterMessageBroker<IOnSelectEventBus>(options);
        }
    }
}