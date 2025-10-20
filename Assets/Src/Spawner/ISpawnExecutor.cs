using GeneralModule.Symbol;

namespace Src.Spawner {
    /// <summary>
    /// スポーン処理を実行するクラスに対して約束するインターフェース
    /// </summary>
    public interface ISpawnExecutor {

        void Spawn();
        
        void SpawnSymbol (ASerializedSymbol symbol);
    }
}