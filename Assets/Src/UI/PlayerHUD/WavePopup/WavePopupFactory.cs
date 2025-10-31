using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.WavePopup {

    public interface IWavePopupFactory {

        void Create(int wave);
        
    }

    public class WavePopupFactory : SerializedMonoBehaviour, IWavePopupFactory {

        [Title("設定")]
        
        [SerializeField]
        [LabelText("表示キャンバス")]
        private Canvas m_canvas;
        
        [SerializeField]
        [LabelText("生成プレファブ")]
        private WavePopup m_prefab;

        [SerializeField]
        [LabelText("表示時間")]
        private float m_lifetime;

        [Title("参照")] 
        [SerializeField]
        private UnityEngine.Camera m_cam;

        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_cam = m_resolver.Resolve<UnityEngine.Camera>();
        }

        public void Create(int wave) {

            var instance = GameObject.Instantiate(
                m_prefab,
                m_canvas.transform
            );
            
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

            instance.SetUp(wave, TimeSpan.FromSeconds(m_lifetime));
            
        }
    }
}