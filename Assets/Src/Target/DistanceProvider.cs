using System;
using UnityEngine;
using VContainer;

namespace Src.Target {
    public class DistanceProvider : IDistanceProvider{

        private ITargetProvider m_target;

        [Inject]
        public DistanceProvider(ITargetProvider target) {
            m_target = target ?? throw new ArgumentNullException();
        }

        public float Distance(GameObject obj) {
            
            if (obj == null) throw new ArgumentNullException();
            
            var target = m_target.Target.CurrentValue;
            
            if (target is null) return 0.0f;
            
            return Vector3.Distance(obj.transform.position, target.transform.position);
        }
    }
}