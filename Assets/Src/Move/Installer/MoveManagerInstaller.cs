using System;
using Sirenix.OdinInspector;
using Src.Utility;
using VContainer;
using VContainer.Unity;

namespace Src.Move.Installer {
    public class MoveManagerInstaller : SerializedMonoBehaviour, IInstaller {
        
        public void Install(IContainerBuilder builder) {

            var characterMove = ComponentsUtility.GetComponentFromWhole<ICharacterMoveController>(gameObject) ??
                                throw new ArgumentNullException();
            
            builder
                .RegisterComponent(characterMove)
                .As<ICharacterMoveController>();
            
            var freefall = ComponentsUtility.GetComponentFromWhole<IFreeFallManager>(gameObject)
                           ?? throw new ArgumentNullException();
            
            builder
                .RegisterComponent(freefall)
                .As<IFreeFallManager>();

            var motion = ComponentsUtility.GetComponentFromWhole<IMotionMoveManager>(gameObject) ?? throw new ArgumentNullException();

            builder
                .RegisterComponent(motion)
                .As<IMotionMoveManager>();
        }
        
    }
}