using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Src.UI.PlayerHUD.GameOver {

    public interface IGameOverPopup {

        void SetUp(int wave);
    }
    
    public class GameOverPopup : SerializedMonoBehaviour, IGameOverPopup {

        [Title("ランタイム")] 
        
        [SerializeField]
        [LabelText("到達ウェーブ")]
        [ReadOnly]
        private int m_archiveWave = 0;

        [Title("参照")]
        
        [SerializeField]
        [LabelText("ウェーブ数表示")]
        private TMP_Text m_text;

        public void SetUp(int wave) {
            
        }
        
        
    }
    
}