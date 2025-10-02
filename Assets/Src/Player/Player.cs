using GeneralModule.Symbol;
using GeneralModule.Symbol.Interface;
using Sirenix.OdinInspector;

namespace Src.Player {

    public interface IPlayer : ISymbol {
        
    }
    
    public class Player : ASerializedSymbol , IPlayer {
        
    }
}