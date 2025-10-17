using MessagePipe;
using Src.Health.EventBus;
using VContainer;
using VContainer.Unity;

namespace Src.Health.Installer {
    public class ManagerStatusInstaller : IInstaller {

        public void Install(IContainerBuilder builder) {
            
            var option = builder.RegisterMessagePipe();

            builder.RegisterMessageBroker<ITakeDamageEventBus>(option);
            
            builder.RegisterMessageBroker<IOnDeadEventBus>(option);
            
        }
    }
}