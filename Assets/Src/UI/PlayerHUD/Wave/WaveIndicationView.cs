using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Src.UI.PlayerHUD.Wave {

    public interface IWaveIndicationView {

        void UpdateWave(int nextWave);
    }
    
    public class WaveIndicationView :  SerializedMonoBehaviour, IWaveIndicationView {
        
        [Title("ランタイム")]
        
        [SerializeField]
        [ReadOnly]
        private int m_currentWave;

        [Title("参照")]
        
        [SerializeField]
        private TMP_Text m_textView;

        public void UpdateWave(int nextWave) {
            
            m_currentWave = nextWave;
            
            m_textView.text = nextWave.ToString();
            
        }
    }
}