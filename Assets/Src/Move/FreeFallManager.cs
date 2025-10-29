using System;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Src.Move {

    public interface IFreeFallManager : IMovementManager {
        
        public void SetEnabled(bool enabled);
        
    }
    
    [Serializable]
    public class FreeFallManager : SerializedMonoBehaviour, IFreeFallManager {

        [Title("設定")] 
        
        [SerializeField]
        [LabelText("設置時にかかる力")]
        private float m_defaultForce = -2.0f;
        
        [Title("ランタイム")]
        
        [SerializeField]
        [LabelText("自由落下が有効か")]
        private bool m_freeFallEnable = true;
        
        [SerializeField]
        [ReadOnly]
        private Vector3 m_movement = Vector3.zero;
        
        [Title("参照")]
        
        [SerializeField]
        [ReadOnly]
        private CharacterController m_characterController;
        
        private IObjectResolver m_resolver;
        
        public Vector3 Movement => m_movement;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            
            m_characterController = m_resolver.Resolve<CharacterController>();
            
            RegisterGrounded();
            
        }

        private void RegisterGrounded() {
            Observable
                .EveryUpdate()
                .Subscribe(_ => {
                    
                    if (!m_freeFallEnable) {
                        m_movement = Vector3.zero;
                        return;
                    }

                    if (m_characterController.isGrounded) {
                        m_movement = new Vector3(0.0f, m_defaultForce, 0.0f);
                    }
                    else {
                        m_movement = Physics.gravity;
                    }
                    
                })
                .AddTo(this);
        }
        

        public void SetEnabled(bool enable) {
            m_freeFallEnable = enable;
        }
        
    }
}