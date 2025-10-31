using Sirenix.OdinInspector;
using UnityEngine;

namespace Src.UI.PlayerHUD.GameOver {

    public interface IGameOverPopupFactory {

        IGameOverPopup Create(int wave);
        
    }
    
    public class GameOverPopupFactory : SerializedMonoBehaviour{
        
        [Title("設定")]

        [SerializeField]
        [LabelText("生成プレファブ")]
        public GameOverPopup m_prefab;
        
        [Title("参照")]
        
        [SerializeField]
        [LabelText("表示キャンバス")]
        private Canvas m_canvas;

        public IGameOverPopup Create(int wave) {

            var instance = Instantiate(m_prefab);
            
            // 4. インスタンスのRectTransformを取得
            if (instance.TryGetComponent<RectTransform>(out RectTransform rt))
            {
                // 5. アンカーポジションを (0, 0) に設定して中心に配置
                // ※前提: uiPrefabのアンカーが中央(0.5, 0.5)であること
                rt.anchoredPosition = Vector2.zero;

                // 6. (推奨) スケールとZ座標をリセット
                // 親のスケールや設定に関わらず、意図した表示を担保します。
                rt.localScale = Vector3.one;
                // anchoredPositionはX, Yのみ設定するため、Z座標は明示的に0にリセット
                rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y, 0f);
            }
            else
            {
                Debug.LogError("UIプレハブのルートにRectTransformがありません。");
            }
            
            instance.transform.SetParent(m_canvas.transform, true);
            
            instance.SetUp(wave);

            return instance;
        }
    }
    
}