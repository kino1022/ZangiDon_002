using System;
using Sirenix.OdinInspector;
using Src.Utility;
using VContainer;
using VContainer.Unity;

namespace Src.Health.Installer {
    public class CharacterStatusInstaller : SerializedMonoBehaviour, IInstaller {

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