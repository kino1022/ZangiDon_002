using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.UI.PlayerHUD.Health {
    public interface IHealthUIView {
        void UpdateHealthValue(int current, int max);
    }

    public class HealthUIView : SerializedMonoBehaviour, IHealthUIView {
        
        [SerializeField]
        [LabelText("バーのグラデーション")]
        [InfoBox("体力の割合をもとに1~0の間で設定すること")]
        private Gradient m_gradient;

        [SerializeField]
        [LabelText("数値表示")]
        private TMP_Text m_textView;

        [SerializeField]
        [LabelText("可変部分のイメージ")]
        private Image m_barImage;
        
        [SerializeField]
        [LabelText("現在値")]
        [ReadOnly]
        private int m_currentHealth;
        
        [SerializeField]
        [LabelText("最大値")]
        [ReadOnly]
        private int m_maxHealth;

        [SerializeField]
        [LabelText("割合")]
        [ReadOnly]
        private float m_ratio;

        public void UpdateHealthValue(int current, int max) {
            
            m_currentHealth = current;
            
            m_maxHealth = max;

            m_ratio = CalculateRatio();
            
            UpdateTextView();
            
            UpdateBarImage();
        }

        private float CalculateRatio() {
            return m_currentHealth / m_maxHealth;
        }

        private void UpdateTextView() {
            m_textView.text = $"{m_currentHealth} / {m_maxHealth}";
        }

        private void UpdateBarImage() {
            var color = m_gradient.Evaluate(m_ratio);
            m_barImage.color = color;
            m_barImage.fillAmount = m_ratio;
        }
    }
}