using GeneralModule.Symbol;
using UnityEngine;
using VContainer;

namespace Src.Spawner {
    /// <summary>
    /// スポーンするオブジェクトを渡すクラスに対して約束するインターフェース
    /// </summary>
    public interface ISpawnSymbolProvider {

        ASerializedSymbol Provide(GameObject spawner, IObjectResolver resolver);
        
    }
}