using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health;
using System;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Health {
    
    public interface IHealthUIPresenter : IStartable, IDisposable {

    }

    [Serializable]
    public class HealthUIPresenter : IHealthUIPresenter {

        [Title("�Q��")]

        [LabelText("�ő�̗�")]
        [OdinSerialize]
        [ReadOnly]
        private IMaxHealth m_maxModel;

        [LabelText("���ݑ̗�")]
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
            m_disposable = new CompositeDisposable();
            RegisterChangeCurrentValue();
            RegisterChangeMaxValue();
        }

        public void Dispose () {
            m_disposable?.Dispose();
        }

        private void RegisterChangeCurrentValue () {
            m_model
                .Value
                .Subscribe(_ => {
                    m_view.UpdateHealthValue(m_model.Value.CurrentValue, m_maxModel.Value.CurrentValue);
                })
                .AddTo(m_disposable);
        }


        private void RegisterChangeMaxValue() {
            m_maxModel
                 .Value
                 .Subscribe(_ => {
                     m_view.UpdateHealthValue(m_model.Value.CurrentValue, m_maxModel.Value.CurrentValue);
                 })
                 .AddTo(m_disposable);
        }
    }
}