namespace Src.Move {

    public interface IForceProvider {
        
        float Force { get; }
    }
    
    public interface IForceManager : IForceProvider {
        
        void SetForce(float next);
        
    }
}