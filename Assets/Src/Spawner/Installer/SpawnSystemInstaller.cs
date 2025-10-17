using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using VContainer;
using VContainer.Unity;

namespace Src.Spawner.Installer {
    public class SpawnSystemInstaller : SerializedMonoBehaviour, IInstaller {
        
        [OdinSerialize]
        [LabelText("シンボル供給クラス")]
        private ISpawnSymbolProvider m_symbolProvider;
        
        [OdinSerialize]
        [LabelText("座標供給クラス")]
        private ISpawnPositionProvider m_positionProvider;

        public void Install(IContainerBuilder builder) {
            
            var executor = gameObject.transform.root
                               .GetComponentInChildren<ISpawnExecutor>()
                           ?? throw new NullReferenceException();

            builder
                .RegisterComponent(executor)
                .As<ISpawnExecutor>();
            
            builder
                .RegisterInstance(m_symbolProvider)
                .As<ISpawnSymbolProvider>();
            
            builder
                .RegisterInstance(m_positionProvider)
                .As<ISpawnPositionProvider>();
        }
    }
}