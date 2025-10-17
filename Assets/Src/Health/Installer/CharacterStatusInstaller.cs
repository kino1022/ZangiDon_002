using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Utility;
using VContainer;
using VContainer.Unity;

namespace Src.Health.Installer {
    public class CharacterStatusInstaller : SerializedMonoBehaviour, IInstaller {
        
        private IObjectResolver m_resolver;
        
        [Title("インスタンス")]
        
        [OdinSerialize]
        [ReadOnly]
        private IDamageable m_damageable;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        private void Start() {
            m_damageable = m_resolver.Resolve<IDamageable>() ?? throw new NullReferenceException();
        }

        public void Install(IContainerBuilder builder) {
            
            var health = ComponentsUtility.GetComponentsFromWhole<IHealth>(gameObject) ??
                         throw new NullReferenceException();

            builder
                .RegisterComponent(health)
                .As<IHealth>();
            
            var max = ComponentsUtility.GetComponentsFromWhole<MaxHealth>(gameObject) ??
                      throw new NullReferenceException();
            
            builder
                .RegisterComponent(max)
                .As<IMaxHealth>();
            
            builder
                .Register<IDamageable, DamageModule>(Lifetime.Singleton)
                .As<IDamageable>()
                .WithParameter(gameObject.transform.root);
        }
    }
}