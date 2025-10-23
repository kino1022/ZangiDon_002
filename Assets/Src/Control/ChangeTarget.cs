using GeneralModule.Symbol;
using R3;
using RinaInput.Controller.Module;
using RinaInput.Signal;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.GameManager.Entities;
using Src.Player;
using Src.Target;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace Src.Control {
    public class ChangeTarget : SerializedMonoBehaviour {


        [Title("ランタイムデータ")]

        [OdinSerialize]
        [LabelText("選択中ターゲット")]
        [ReadOnly]
        private ASerializedSymbol m_selectTarget;

        [Title("入力モジュール")]

        [OdinSerialize]
        [LabelText("調整スティック")]
        private IInputModule<Vector2> m_selectStick;

        [OdinSerialize]
        [LabelText("確定トリガー")]
        private IInputModule<float> m_decideTrigger;

        [Title("参照")]

        [OdinSerialize]
        private Player.Player m_player;

        [OdinSerialize]
        [ReadOnly]
        private IEntitiesProvider m_entitiesProvider;

        [OdinSerialize]
        [ReadOnly]
        private ITargetManager m_targetManager;

        private IObjectResolver m_resolver;

        [Inject]
        public void Construct (IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {

            m_targetManager = m_resolver.Resolve<ITargetManager>();

            m_entitiesProvider = m_resolver.Resolve<IEntitiesProvider>();

            RegisterInput();
        }

        private void RegisterInput () {

            if (m_selectStick is null) throw new System.NullReferenceException();

            if (m_decideTrigger is null) throw new System.NullReferenceException();

            m_selectStick
                .Stream
                .Where(x => x.Phase != UnityEngine.InputSystem.InputActionPhase.Canceled)
                .Subscribe(OnStickInput)
                .AddTo(this);

            m_decideTrigger
                .Stream
                .Where(x => x.Phase != UnityEngine.InputSystem.InputActionPhase.Canceled)
                .Subscribe(OnTriggerInput)
                .AddTo(this);
        }

        private void OnStickInput (InputSignal<Vector2> signal) {

        }

        private void OnTriggerInput (InputSignal<float> signal) {

        }

        private ASerializedSymbol CalculateTarget (Vector2 input) {

            var entities = m_entitiesProvider.Entities ?? throw new System.ArgumentNullException();

            if (entities.Count <= 2) return entities.First();

            if (input == Vector2.zero) return InitialiTarget(entities);

            return null;
        }
        
        private ASerializedSymbol InitialiTarget (IReadOnlyList<ASerializedSymbol> symbols) {

            if (symbols is null || symbols.Count is 0) return null;

            var result = symbols.First();

            var distance = Vector3.Distance(m_player.gameObject.transform.position, result.transform.position);

            foreach (var symbol in symbols) {

                var tempDistance = Vector3.Distance(m_player.gameObject.transform.position, symbol.transform.position);

                if (tempDistance < distance) {
                    result = symbol;
                    distance = tempDistance;
                }
            }

            return result;
        }
    }
}