using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Health {

    public interface IHealthUIPresenter : IStartable, IDisposable {

    }

    public class HealthUIPresenter : IHealthUIPresenter {

        [Title("éQè∆")]

        [LabelText("ç≈ëÂëÃóÕ")]
        [OdinSerialize]
        [ReadOnly]
        private IMaxHealth m_maxModel;

        [LabelText("åªç›ëÃóÕ")]
        [OdinSerialize]
        [ReadOnly]
        private IHealth m_model;

        [LabelText("View")]
        [OdinSerialize]
        [ReadOnly]
        private IHealthUIView m_view;

        private CompositeDisposable m_disposable;

        [Inject]
        public HealthUIPresenter(IMaxHealth max, IHealth current, IHealthUIView view) {
            m_maxModel = max ?? throw new ArgumentNullException(nameof(max));
            m_model = current ?? throw new ArgumentNullException(nameof(current));
            m_view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Start() {

        }

        public void Dispose () {

        }
    }
}