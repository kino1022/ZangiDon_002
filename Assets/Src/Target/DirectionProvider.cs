using System;
using UnityEngine;
using VContainer;

namespace Src.Target {
    public class DirectionProvider : IDirectionProvider {
        
        private ITargetProvider m_targetProvider;

        [Inject]
        public DirectionProvider(ITargetProvider targetProvider) {
            m_targetProvider = targetProvider ?? throw new ArgumentNullException();
        }

        public Vector3 Direction(GameObject obj) {
            
            if (obj is null) throw new ArgumentNullException();

            var target = m_targetProvider.Target.CurrentValue;
            
            if (target is null) return Vector3.zero;
            
            return (obj.transform.position - target.transform.position).normalized;
            
        }
    }
}