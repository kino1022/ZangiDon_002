using GeneralModule.Scope;
using Src.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Enemy {
    public class EnemyLifetimeScope : ListableLifetimeScope{

        protected override void Configure(IContainerBuilder builder) {
            base.Configure(builder);
            
            var cc = ComponentsUtility.GetComponentsFromWhole<CharacterController>(gameObject);

            if (cc is not null) {
                builder
                    .RegisterComponent(cc)
                    .As<CharacterController>();
            }
        }
    }
}