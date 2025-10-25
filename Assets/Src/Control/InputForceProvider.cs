using R3;
using RinaInput.Controller.Module;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Src.Control {
    
    public interface IInputForceProvider {
        
        /// <summary>
        /// 入力の強さ
        /// </summary>
        float InputForce { get; }
        
    }

    public class InputForceProvider : SerializedMonoBehaviour, IInputForceProvider {
        
        [OdinSerialize]
        [LabelText("スティック")]
        private IInputModule<Vector2> m_inputModule;
        
        [SerializeField]
        [LabelText("入力の強さ")]
        [ReadOnly]
        private float m_inputForce;

        public float InputForce => m_inputForce;

        private void Start() {
            if (m_inputModule is null) {
                throw new System.ArgumentNullException();
            }

            RegisterInputModule();
        }

        private void RegisterInputModule() {
            m_inputModule
                .Stream
                .Do(onDispose: () => m_inputForce = 0.0f)
                .Subscribe(x => {

                    if (x.Phase == UnityEngine.InputSystem.InputActionPhase.Canceled) {
                        m_inputForce = 0.0f;
                        return;
                    }

                    m_inputForce = x.Value.magnitude;
                })
                .AddTo(this);
        }
    }
}