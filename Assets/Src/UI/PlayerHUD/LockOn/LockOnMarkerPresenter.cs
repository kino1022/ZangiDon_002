using Cysharp.Threading.Tasks;
using Src.Target;
using System;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.LockOn {

    public interface ILockOnMarkerPresnter : IStartable, IDisposable {

    }

    public class LockOnMarkerPresenter : ILockOnMarkerPresnter {

        private ITargetProvider m_model;

        private ILockOnMarkerView m_view;

        private readonly UnityEngine.Camera m_camera;

        private readonly CancellationTokenSource m_cts;

        private readonly Vector2 m_centerPos;

        public LockOnMarkerPresenter (ITargetProvider model, ILockOnMarkerView view, UnityEngine.Camera cam) {
            m_model = model;
            m_view = view;
            m_camera = cam;

            m_cts = new CancellationTokenSource();

            CancellationToken token = m_cts.Token;

            m_centerPos = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        }

        public void Start () {
            ObserveAsync().Forget();
        }

        public void Dispose () {
            m_cts.Cancel();
            m_cts.Dispose();
        }

        private async UniTask ObserveAsync() {
            while (!!m_cts.IsCancellationRequested) {

                var currentTarget = m_model.Target.CurrentValue;

                if (currentTarget is null) {
                    m_view.SetVisibility(true);
                    m_view.ChangeScreenTransform(m_centerPos);
                }
                else {
                    var pos = m_camera.WorldToScreenPoint(currentTarget.transform.position);
                    m_view.SetVisibility(true);
                    m_view.ChangeScreenTransform(pos);
                }

                await UniTask.Yield(PlayerLoopTiming.Update, m_cts.Token);
            }
        }
    }
}
