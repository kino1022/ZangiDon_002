using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Src.Health.EventBus;
using TMPro;
using UnityEngine;

namespace Src.UI.PlayerHUD.Damage {

    public interface IDamagePopup {
        
        void SetUp (ITakeDamageEventBus eventBus, TimeSpan lifetime);
        
    }
    
    public class DamagePopup : SerializedMonoBehaviour, IDamagePopup　{

        [SerializeField]
        [LabelText("テキスト")]
        private TMP_Text m_text;

        [SerializeField]
        [LabelText("値")]
        [ReadOnly]
        private int m_value;
        
        [SerializeField]
        [LabelText("生存期間")]
        [ReadOnly]
        private TimeSpan m_lifetime;

        public void SetUp(ITakeDamageEventBus eventBus, TimeSpan lifetime) {
            m_value = eventBus.Damage.Value;
            m_lifetime = lifetime;
            m_text.text = m_value.ToString();

            AsyncLifetime().Forget();
        }

        private async UniTask AsyncLifetime() {
            
            var token = this.GetCancellationTokenOnDestroy();
            try {
                await UniTask.Delay(m_lifetime, cancellationToken: token);
            }
            catch (OperationCanceledException) {

            }
            finally {
                OnDead();
            }
        }

        private void OnDead() {
            Destroy(gameObject);
        }
        
    }
}