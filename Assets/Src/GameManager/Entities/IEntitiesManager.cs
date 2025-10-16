using GeneralModule.Symbol;

namespace Src.GameManager.Entities {
    public interface IEntitiesManager {
        
        void Add (ASerializedSymbol entity);
        
        void Remove (ASerializedSymbol entity);
        
        void Clear ();
    }
}