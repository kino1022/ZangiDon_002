using UnityEngine;

namespace Src.Target {
    public interface IDistanceProvider {
        
        /// <summary>
        /// 指定したオブジェクトとターゲットの距離を取得する
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        float Distance (GameObject obj);
    }
}