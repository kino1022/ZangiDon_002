using System;
using MessagePipe;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health.EventBus;
using Src.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Health.Installer {
    public class CharacterStatusInstaller : SerializedMonoBehaviour, IInstaller {
        
        private IObjectResolver m_resolver;
        
        [Title("インスタンス")]
        
        [OdinSerialize]
        [ReadOnly]
        private IDamageable m_damageable;
        
        private Func<GameObject, IDamageable> m_damageableFactory;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        private void Start() {
            m_damageableFactory = m_resolver.Resolve<Func<GameObject, IDamageable>>();
            m_damageable = m_damageableFactory.Invoke(gameObject.transform.root.gameObject);
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

            builder.RegisterFactory<GameObject, IDamageable>(resolver => {
                return param => {
                    var health = resolver.Resolve<IHealth>();
                    var subscriber = resolver.Resolve<ISubscriber<ITakeDamageEventBus>>();

                    // 引数の param (GameObject) と Resolve したものを組み合わせて返す
                    // ※DamageModuleがparamを必要とするなら、ここで渡す
                    return new DamageModule(subscriber, health, param);
                };
            }, 
                Lifetime.Singleton
                );
        }
    }
}