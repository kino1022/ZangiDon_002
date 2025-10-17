using UnityEngine;
using VContainer;

namespace Src.Spawner {
    /// <summary>
    /// スポーンする座標を供給するクラスに対して約束するインターフェース
    /// </summary>
    public interface ISpawnPositionProvider {
        
        Vector3 Provide(GameObject spawner, IObjectResolver resolver);
        
    }
}