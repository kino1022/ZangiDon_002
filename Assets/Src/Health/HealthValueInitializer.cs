using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Src.Health {
    [DefaultExecutionOrder(1000)]
    public class HealthValueInitializer : SerializedMonoBehaviour {

        [SerializeField]
        [LabelText("初期値")]
        [ProgressBar(0,1000)]
        private int m_initializeValue = 100;

        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        public void Start() {
            
            var health = m_resolver.Resolve<IHealth>() ?? throw new NullReferenceException();
            
            var maxHealth = m_resolver.Resolve<IMaxHealth>() ?? throw new NullReferenceException();
            
            maxHealth.Set(m_initializeValue);
            
            health.Set(m_initializeValue);
            
        }
    }
}