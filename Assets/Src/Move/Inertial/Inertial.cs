using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Src.Move.Inertial {

    public interface IInertial : IStartable, IDisposable {
        
        Vector3 Movement { get; }
        
    }
    
    public class Inertial : IInertial {

        private float m_force = 1.0f;
        
        private Vector3 m_direction = Vector3.zero;

        private float m_damping = 0.98f;

        private float m_threshold = 0.01f;

        private CancellationTokenSource m_cts;
        
        public Vector3 Movement => m_direction * m_force;

        public void Start() {
            m_cts = new CancellationTokenSource();
            CancellationToken token = m_cts.Token;
            
            UpdateAsync().Forget();
        }

        public void Dispose() {
            m_cts?.Cancel();
            m_cts = null;
        }

        private async UniTask UpdateAsync() {
            try {
                while (m_threshold < m_force || !m_cts.IsCancellationRequested) {

                    await UniTask.Delay(
                        TimeSpan.FromSeconds(0.1f),
                        cancellationToken: m_cts.Token
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