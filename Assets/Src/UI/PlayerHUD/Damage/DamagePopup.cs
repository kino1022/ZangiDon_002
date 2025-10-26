using System;
using System.Threading;
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
        [LabelText("移動スピード")]
        private float m_speed = 2.0f;

        [SerializeField] [LabelText("フェードタイム")]
        private float m_fadeOut = 3.0f;
        
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
                await MovePopup(token);
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

        private async UniTask MovePopup(CancellationToken token) {

            float timer = 0.0f;
            
            Vector3 initPos = transform.position;
            
            Color initialColor = m_text.color;

            while (timer < m_fadeOut)
            {
                // 経過時間で移動とフェードを計算
                float normalizedTime = timer / m_fadeOut;
            
                // 上に移動
                transform.position = initPos + new Vector3(0, normalizedTime * m_speed, 0);
            
                // フェードアウト
                float newAlpha = Mathf.Lerp(initialColor.a, 0f, normalizedTime);
                m_text.color = new Color(initialColor.r, initialColor.g, initialColor.b, newAlpha);

                // 1フレーム待機
                await UniTask.Yield(PlayerLoopTiming.Update, token);
                timer += Time.deltaTime;
            }
        }
        
    }
}