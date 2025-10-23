using System;
using Sirenix.OdinInspector;
using Src.Health.EventBus;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Damage {

    public interface IDamagePopupFactory {
        
        IDamagePopup Create(ITakeDamageEventBus eventBus);
        
    }
    
    public class DamagePopupFactory : SerializedMonoBehaviour, IDamagePopupFactory{
        
        [SerializeField]
        private DamagePopup m_popupPrefab;
        
        [SerializeField]
        private Canvas m_canvas;

        [SerializeField]
        [LabelText("生存期間(秒)")]
        [ProgressBar(0.0f,10.0f)]
        private float m_lifetime;

        [SerializeField]
        [LabelText("カメラ参照")]
        [ReadOnly]
        private UnityEngine.Camera m_camera;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_camera = m_resolver.Resolve<UnityEngine.Camera>();
        }

        public IDamagePopup Create(ITakeDamageEventBus eventBus) {
            
            var pos = m_camera.WorldToScreenPoint(eventBus.Object.transform.position);

            DamagePopup instance = m_resolver.Instantiate(
                m_popupPrefab,
                pos,
                Quaternion.identity
            );
            
            instance.transform.SetParent(m_canvas.transform, true);
            
            instance.SetUp(eventBus, TimeSpan.FromSeconds(m_lifetime));
            
            return instance;
        }
    }
}