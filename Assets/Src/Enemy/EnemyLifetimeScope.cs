using GeneralModule.Scope;
using Src.Bot;
using Src.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Enemy {
    public class EnemyLifetimeScope : ListableLifetimeScope{

        protected override void Configure(IContainerBuilder builder) {
            base.Configure(builder);
            
            var cc = ComponentsUtility.GetComponentFromWhole<CharacterController>(gameObject);

            if (cc is not null) {
                builder
                    .RegisterComponent(cc)
                    .As<CharacterController>();
            }

            var speed = gameObject.GetComponentFromWhole<IEnemySpeed>();

            if (speed is not null) {
                builder
                    .RegisterComponent(speed)
                    .As<IEnemySpeed>();
            }

            builder
                .Register<EnemyTargetProvider>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}