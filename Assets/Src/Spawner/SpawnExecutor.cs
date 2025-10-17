using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.GameManager.Entities;
using VContainer;
using VContainer.Unity;

namespace Src.Spawner {
    public class SpawnExecutor : SerializedMonoBehaviour, ISpawnExecutor {
        
        [Title("参照")]
        
        [OdinSerialize]
        [LabelText("シンボルプロバイダ")]
        [ReadOnly]
        private ISpawnSymbolProvider m_symbolProvider;
        
        [OdinSerialize]
        [LabelText("座標プロバイダ")]
        [ReadOnly]
        private ISpawnPositionProvider m_positionProvider;
        
        [OdinSerialize]
        [LabelText("エンティティマネージャー")]
        [ReadOnly]
        private IEntitiesManager m_entitiesManager;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentException();
        }

        public void Start() {
            
            m_entitiesManager = m_resolver.Resolve<IEntitiesManager>() 
                                ?? throw new NullReferenceException();

            m_positionProvider = m_resolver.Resolve<ISpawnPositionProvider>()
                                 ?? throw new NullReferenceException();
            
            m_symbolProvider = m_resolver.Resolve<ISpawnSymbolProvider>() 
                               ?? throw new NullReferenceException();
        }

        [Button("スポーン")]
        public void Spawn() {
            
            var symbol = m_symbolProvider.Provide() ?? throw new NullReferenceException();
            
            var position = m_positionProvider.Provide(gameObject, m_resolver);

            var instance = m_resolver.Instantiate(
                symbol,
                position,
                gameObject.transform.rotation
            );

            m_entitiesManager.Add(instance);
            
        }
    }
}