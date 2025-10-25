using UnityEngine;

namespace Src.Move {

    public interface IDirectionProvider {
        
        Vector3 Direction { get; }
        
    }
    
    public interface IDirectionManager : IDirectionProvider {

        void SetDirection(Vector3 next);

    }
}