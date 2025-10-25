using System;
using R3;
using RinaInput.Controller.Module;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Src.Control {

    public interface IInputDirectionProvider {
        
        /// <summary>
        /// 入力されている方向
        /// </summary>
        Vector2 InputDirection { get; }
        
    }
    
    public class InputDirectionProvider : SerializedMonoBehaviour, IInputDirectionProvider {
        
        [OdinSerialize]
        [LabelText("入力スティック")]
        private IInputModule<Vector2> m_inputModule;
        
        [SerializeField]
        [LabelText("入力方向")]
        [ReadOnly]
        private Vector2 m_inputDirection;

        public Vector2 InputDirection => m_inputDirection;

        private void Start() {
            
            if (m_inputModule is null) {
                throw new ArgumentNullException();
            }
            
            RegisterInput();
        }

        private void RegisterInput() {

            m_inputModule
                .Stream
                .Subscribe(x => {
                    m_inputDirection = x.Value.normalized;
                })
                .AddTo(this);
        }
    }
}