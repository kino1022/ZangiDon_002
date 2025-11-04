using VContainer;
using VContainer.Unity;

namespace Src.Sound {
    public class SoundSystemInstaller : IInstaller {

        public void Install(IContainerBuilder builder) {
            builder
                .Register<SoundManager>(Lifetime.Transient)
                .AsImplementedInterfaces();
        }
    }
}