using R3;
using Sirenix.OdinInspector;
using Src.Camera.Definition;

namespace Src.Camera {
    public class ModeManager : SerializedMonoBehaviour, ICameraModeManager {
        
        private ReactiveProperty<CameraMode> m_mode = new ReactiveProperty<CameraMode>(CameraMode.FirstPerson);
        
        public ReadOnlyReactiveProperty<CameraMode> Mode => m_mode;
        
        [ShowInInspector]
        public CameraMode InspectorMode => m_mode.Value;

        public void ChangeMode(CameraMode mode) {
            m_mode.Value = mode;
        }
        
    }
}