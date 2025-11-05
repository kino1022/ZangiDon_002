using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer.Unity;

namespace Src.Move.Inertial {

    /// <summary>
    /// 慣性を表現するクラスに対して約束するインターフェース
    /// </summary>
    public interface IInertial : IForceProvider, IDirectionProvider, IStartable, IDisposable {
        
        Vector3 Movement { get; }
        
    }
    
    [Serializable]
    public class Inertial : IInertial {

        [SerializeField]
        [LabelText("速度")]
        private float m_force = 1.0f;
        
        [SerializeField]
        [LabelText("方向")]
        private Vector3 m_direction = Vector3.zero;

        [SerializeField]
        [LabelText("減衰率")]
        private float m_damping = 0.98f;

        [SerializeField]
        [LabelText("閾値")]
        private float m_threshold = 0.01f;

        private CancellationTokenSource m_cts;
        
        public Vector3 Movement => m_direction * m_force;
        
        public float Force => m_force;
        
        public Vector3 Direction => m_direction;

        public Inertial(Vector3 argdir, float argforce, float argdamp) {
            
            //減衰率が0未満または1超過の場合は例外をスロー
            if (argdamp < 0.0f || argdamp > 1.0f) {
                throw new ArgumentOutOfRangeException(nameof(argdamp), "Damping must be between 0 and 1.");
            }

            //与えられた力がマイナスだった場合は方向を反転して絶対値を利用
            if (argforce < 0.0f) {
                argforce = Mathf.Abs(argforce);
                argdir *= -1.0f;
            }
            
            m_direction = argdir.normalized;
            
            m_force = argforce;
            
            m_damping = argdamp;
            
        }

        public void Start() {
            
            m_cts = new CancellationTokenSource();
            
            CancellationToken token = m_cts.Token;
            
            UpdateAsync(m_cts.Token).Forget();
            
        }

        public void Dispose() {
            m_cts?.Cancel();
            m_cts?.Dispose();
            m_cts = null;
        }

        private async UniTask UpdateAsync(CancellationToken token) {
            try {
                while (m_threshold < m_force && !token.IsCancellationRequested) {

                    await UniTask.Delay(
                        TimeSpan.FromSeconds(0.1f),
                        cancellationToken:token
                    );
                    m_force *= m_damping;
                    
                }
            }
            catch (OperationCanceledException) {

            }
            finally {
                m_force = 0.0f;
            }
        }
    }
}