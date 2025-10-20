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
        [LabelText("生存期間(秒)")]
        [ProgressBar(0.0f,10.0f)]
        private float m_lifetime;

        private UnityEngine.Camera m_camera;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        public IDamagePopup Create(ITakeDamageEventBus eventBus) {
            var pos = m_camera.WorldToScreenPoint(eventBus.Object.transform.position);

            IDamagePopup instance = m_resolver.Instantiate(
                m_popupPrefab,
                pos,
                Quaternion.identity
            );
            
            instance.SetUp(eventBus, TimeSpan.FromSeconds(m_lifetime));
            
            return instance;
        }
    }
}