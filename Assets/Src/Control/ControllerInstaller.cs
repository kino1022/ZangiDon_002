using System;
using Sirenix.OdinInspector;
using Src.Utility;
using VContainer;
using VContainer.Unity;

namespace Src.Control {
    public class ControllerInstaller : SerializedMonoBehaviour, IInstaller {

        public void Install(IContainerBuilder builder) {
            
            var force = ComponentsUtility.GetComponentsFromWhole<IInputForceProvider>(gameObject) ??
                        throw new ArgumentNullException();

            builder
                .RegisterComponent(force)
                .As<IInputForceProvider>();
            
            var direction = ComponentsUtility.GetComponentsFromWhole<IInputDirectionProvider>(gameObject) 
                            ?? throw new ArgumentNullException();
            
            builder
                .RegisterComponent(direction)
                .As<IInputDirectionProvider>();
            
        }
        
    }
}