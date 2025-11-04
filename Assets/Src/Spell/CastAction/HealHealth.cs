using System;
using System.Runtime.Serialization;
using MessagePipe;
using Sirenix.OdinInspector;
using Src.Health;
using Src.Health.EventBus;
using Src.Spell.CastAction.Interface;
using UnityEngine;
using VContainer;

namespace Src.Spell.CastAction {
    [Serializable]
    public class HealHealth : IPreCastAction {

        [Title("設定")]
        
        [SerializeField] 
        [LabelText("回復量")] 
        private int m_value = 10;

        public void Action(GameObject caster, IObjectResolver resolver) {
            var heal = new Heal(m_value);
            var eventBus = new TakeHealEventBus(caster, heal);
            
            var publisher = resolver.Resolve<IPublisher<IHealEventBus>>() 
                            ?? throw new NullReferenceException();
            
            publisher.Publish(eventBus);
        }
    }
}