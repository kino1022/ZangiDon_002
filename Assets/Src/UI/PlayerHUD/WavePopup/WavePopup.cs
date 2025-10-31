using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Src.UI.PlayerHUD.WavePopup {

    public interface IWavePopup {
        
        void SetUp (int wave, TimeSpan lifetime);
        
    }
    
    public class WavePopup : SerializedMonoBehaviour, IWavePopup {
        
        [SerializeField]
        private TMP_Text m_text;

        private int m_wave;

        private TimeSpan m_lifetime;

        public void SetUp(int wave, TimeSpan lifetime) {
            
            m_wave = wave;
            
            m_lifetime = lifetime;
            
            m_text.text = $"Wave {m_wave} Start !!"; 
            
            AsyncLifetime().Forget();
            
        }

        private async UniTask AsyncLifetime() {
            
            var token = this.GetCancellationTokenOnDestroy();

            var timer = 0.0f;
            
            Color initColor = m_text.color;

            while (!token.IsCancellationRequested || timer < m_lifetime.Seconds) {
                
                float normalizedTime = timer / m_lifetime.Seconds;
                
                float newAlpha = Mathf.Lerp(initColor.a, 0, normalizedTime);
                m_text.color = new Color(initColor.r, initColor.g, initColor.b, newAlpha);
                
                await UniTask.Yield(PlayerLoopTiming.Update, token);
                timer += Time.deltaTime;
            }
            
            OnDead();
        }

        private void OnDead() {
            Destroy(gameObject);
        }
    }
}