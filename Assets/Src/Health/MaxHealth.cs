

using RinaStatus;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Src.Health {

    public interface IMaxHealth : ICorrectableStatus<int> {
        
    }
    
    public class MaxHealth : ACorrectableStatus<int> , IMaxHealth {
        
        [Title("参照")]
        
        [SerializeField]
        [LabelText("初期値")]
        private int m_initValue = 100;
        
        protected override void Start() {
            base.Start();
            
            m_rawValue.Set(m_initValue);
            
            m_correctedValue.Set(m_initValue);
        }
    }
    
}