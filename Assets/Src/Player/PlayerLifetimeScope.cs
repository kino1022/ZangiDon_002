using System;
using GeneralModule.Scope;
using MessagePipe;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.GameManager.Entities;
using Src.Sound;
using Src.Spell.EventBus.Interface;
using Src.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Player {
    public class PlayerLifetimeScope : ListableLifetimeScope {
        
        [SerializeReference]
        [ReadOnly]
        private ISoundManager m_soundManager;
        
        protected void Start() {
            
            m_soundManager = Container.Resolve<ISoundManager>() ?? throw new NullReferenceException();
            
        }

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

            var audio = gameObject.GetComponentFromWhole<AudioSource>()
                        ?? throw new ArgumentNullException();

            if (cc is not null) {
                builder
                    .RegisterComponent(audio)
                    .As<AudioSource>();
            }
        }
    }
}