using Sirenix.OdinInspector;
using UnityEngine;

namespace Src.Move {

    public interface IMotionMoveManager : IMovementManager, IForceManager, IDirectionManager  {


    }
    
    public class MotionMoveManager : AMovementManager, IMotionMoveManager {
        
        
        
    }
}