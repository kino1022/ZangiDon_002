using System;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Camera.Definition;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace Src.Camera {
    public class CameraSelector : SerializedMonoBehaviour {


        [Title("カメラ")]
        
        [SerializeField]
        [LabelText("主観カメラ")]
        private CinemachineCamera m_fpsCamera;
        
        [SerializeField]
        [LabelText("三人称カメラ")]
        private CinemachineCamera m_tpsCamera;

        [Title("優先度")]
        
        [SerializeField]
        [LabelText("通常の優先度")]
        private int m_defaultPriority = 0;

        [SerializeField]
        [LabelText("選択時の優先度")]
        private int m_selectedPriority = 20;
        
        [Title("参照")]
        
        [OdinSerialize]
        [LabelText("カメラモード")]
        [ReadOnly]
        private ICameraModeManager m_modeManager;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        public void Start() {
            
            m_modeManager = m_resolver.Resolve<ICameraModeManager>() ?? throw new NullReferenceException();
            
            RegisterCameraMode();
        }


        private void RegisterCameraMode() {
            m_modeManager
                .Mode
                .Subscribe(x => {
                    if (x == CameraMode.FirstPerson) {
                        m_fpsCamera.Priority = m_selectedPriority;
                        m_tpsCamera.Priority = m_defaultPriority;
                    }

                    if (x == CameraMode.ThirdPerson) {
                        m_fpsCamera.Priority = m_defaultPriority;
                        m_tpsCamera.Priority = m_defaultPriority;
                    }
                })
                .AddTo(this);
        }
    }
}