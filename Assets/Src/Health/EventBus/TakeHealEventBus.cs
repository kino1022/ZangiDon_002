using System;
using UnityEngine;

namespace Src.Health.EventBus {

    public interface IHealEventBus {
        GameObject Target { get; }
        
        IHeal Heal { get; }
    }
    public readonly struct TakeHealEventBus : IHealEventBus {
        
        private readonly GameObject m_target;
        
        private readonly IHeal m_heal;
        
        public GameObject Target => m_target;
        
        public IHeal Heal => m_heal;

        public TakeHealEventBus(GameObject obj, IHeal heal) {
            
            m_target = obj ?? throw new ArgumentNullException();
            
            m_heal = heal ?? throw new ArgumentNullException();
        }
    }
    
}