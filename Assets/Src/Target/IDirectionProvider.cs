using UnityEngine;

namespace Src.Target {
    public interface IDirectionProvider {
        
        /// <summary>
        /// 指定したオブジェクトから見たターゲットの方向を取得する
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Vector3 Direction (GameObject obj);
        
    }
}