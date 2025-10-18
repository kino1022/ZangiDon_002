

using RinaStatus;

namespace Src.Health {

    public interface IMaxHealth : ICorrectableStatus<int> {
        
    }
    
    public class MaxHealth : ACorrectableStatus<int> , IMaxHealth {
        
    }
    
}