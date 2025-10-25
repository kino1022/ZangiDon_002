using UnityEngine;
using VContainer.Unity;

namespace Src.Move {

    public interface IMovementProvider {
        
        Vector3 Movement { get; }
    }
    
    public interface IMovementManager : IMovementProvider {
        
        
        
    }
    
}