using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Utility;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Src.UI.PlayerHUD.Health {
    [DefaultExecutionOrder(1000)]
    public class HealthBarInstaller : SerializedMonoBehaviour, IInstaller {

        [OdinSerialize]
        [LabelText("ÉvÉåÉ[ÉìÉ^Å[")]
        private IHealthUIPresenter m_barPresnter;

        private IObjectResolver m_resolver;

        [Inject]
        public void Construct (IObjectResolver resolver) {
            m_resolver = resolver ?? throw new System.ArgumentNullException();
        }

        private void Start() {
            m_barPresnter = m_resolver.Resolve<IHealthUIPresenter>() ?? throw new System.ArgumentNullException();

            m_barPresnter.Start();
        }

        public void Install (IContainerBuilder builder) {

            builder.RegisterComponent(ComponentsUtility.GetComponentsFromWhole<IHealthUIView>(gameObject)).As<IHealthUIView>();

            builder.Register<IHealthUIPresenter, HealthUIPresenter>(Lifetime.Transient);

        }
    }
}