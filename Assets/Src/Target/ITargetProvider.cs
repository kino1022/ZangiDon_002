using R3;
using UnityEngine;

namespace Src.Target {
    /// <summary>
    /// プレイヤーのターゲットを管理するクラスに対して約束するインターフェース
    /// </summary>
    public interface ITargetProvider {
        
        ReadOnlyReactiveProperty<GameObject> Target { get; }
        
    }
}