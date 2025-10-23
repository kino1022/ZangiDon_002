using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Move.Inertial;

namespace Src.Move {

    public interface ICharacterMoveController {
        
    }
    
    public class CharacterMoveController : SerializedMonoBehaviour, ICharacterMoveController {

        [OdinSerialize]
        [LabelText("慣性管理クラス")]
        [ReadOnly]
        private IInertialManager m_inertialManager;
        
        [OdinSerialize]
        [LabelText("自由落下制御")]
        [ReadOnly]
        private IFreeFallManager m_freeFallManager;
        
    }
}