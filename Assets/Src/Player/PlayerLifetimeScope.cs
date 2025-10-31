using GeneralModule.Scope;
using MessagePipe;
using Src.Spell.EventBus.Interface;
using Src.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Player {
    public class PlayerLifetimeScope : ListableLifetimeScope {

        protected override void Configure(IContainerBuilder builder) {
            base.Configure(builder);
            
            var animator = ComponentsUtility.GetComponentFromWhole<Animator>(gameObject);
            
            if (animator is not null) {
                builder
                    .RegisterComponent(animator)
                    .As<Animator>();
            }
            
            var cc = ComponentsUtility.GetComponentFromWhole<CharacterController>(gameObject);

            if (cc is not null) {
                builder
                    .RegisterComponent(cc)
                    .As<CharacterController>();
            }
        }
    }
}