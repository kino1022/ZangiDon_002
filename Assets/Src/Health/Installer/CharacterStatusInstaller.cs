using System;
using MessagePipe;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health.EventBus;
using Src.Health.Src.Health;
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
        
        [OdinSerialize]
        [ReadOnly]
        private IHealable m_healable;
        

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        private void Start() {
            
            m_damageable = m_resolver.Resolve<IDamageable>();
            
            
            m_healable = m_resolver.Resolve<IHealable>();
        }

        public void Install(IContainerBuilder builder) {
            
            var health = ComponentsUtility.GetComponentFromWhole<IHealth>(gameObject) ??
                         throw new NullReferenceException();

            builder
                .RegisterComponent(health)
                .As<IHealth>();
            
            var max = ComponentsUtility.GetComponentFromWhole<MaxHealth>(gameObject) ??
                      throw new NullReferenceException();
            
            builder
                .RegisterComponent(max)
                .As<IMaxHealth>();

            builder
                .Register<IDamageable, DamageModule>(Lifetime.Scoped)
                .AsImplementedInterfaces();
            
            builder
                .Register<IHealable, HealModule>(Lifetime.Scoped)
                .AsImplementedInterfaces();
        }
    }
}