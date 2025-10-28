using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Camera {

    public interface ICameraDirectionProvider {

        (Vector3 front, Vector3 right) Provide();
        
    }
    
    [Serializable]
    public class CameraDirectionProvider : ICameraDirectionProvider, IStartable {

        [Title("参照")]
        
        [SerializeField]
        [ReadOnly]
        private UnityEngine.Camera m_cam;

        private IObjectResolver m_resolver;

        public CameraDirectionProvider(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        public void Start() {
            m_cam = m_resolver.Resolve<UnityEngine.Camera>();
        }
        
        public (Vector3 front, Vector3 right) Provide() {
            var front = m_cam.transform.forward;
            front.y = 0;
            front.Normalize();
            
            var right = m_cam.transform.right;
            right.y = 0;
            right.Normalize();
            
            return (front, right);
        }
        
    }
}