using R3;
using Src.Camera.Definition;

namespace Src.Camera {
    public interface ICameraModeManager {
        
        ReadOnlyReactiveProperty<CameraMode> Mode { get; }
        
        void ChangeMode(CameraMode mode);
        
    }
}