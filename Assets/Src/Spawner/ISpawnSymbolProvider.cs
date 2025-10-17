using GeneralModule.Symbol;

namespace Src.Spawner {
    /// <summary>
    /// スポーンするオブジェクトを渡すクラスに対して約束するインターフェース
    /// </summary>
    public interface ISpawnSymbolProvider {

        ASerializedSymbol Provide();
        
    }
}