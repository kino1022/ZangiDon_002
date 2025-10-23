using R3;
using RinaInput.Controller.Module;
using RinaInput.Lever.Direction.Definition;
using RinaInput.Signal;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Manager.Selector.Interface;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Src.Control {

    public interface ISpellSelectAction {
        
        public int SelectIndex { get; }
        
    }
    
    public class SpellSelect : SerializedMonoBehaviour, ISpellSelectAction {
        
        [Title("入力モジュール")]

        [OdinSerialize]
        [LabelText("選択用スティック")]
        private IInputModule<Vector2> m_stick;

        [OdinSerialize]
        [LabelText("決定用トリガー")]
        private IInputModule<float> m_trigger;

        [SerializeField]
        [LabelText("選択中インデックス")]
        [ReadOnly]
        private int m_selectIndex = 0;
        
        [Title("参照")]

        [OdinSerialize]
        [LabelText("セレクター")]
        [ReadOnly]
        private ISpellSelector m_selector;
        
        private IObjectResolver m_resolver;
        
        public int SelectIndex => m_selectIndex;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        public void Start() {
            
            m_selector = m_resolver.Resolve<ISpellSelector>();
            
            RegisterStream();
            
        }

        private void RegisterStream() {
            
            m_stick
                .Stream
                .Where(x => x.Phase != InputActionPhase.Canceled)
                .Subscribe(x => {
                    m_selectIndex = CalculateIndex(x);
                })
                .AddTo(this);
            
            m_trigger
                .Stream
                .Where(x => x.Phase != InputActionPhase.Canceled)
                .Subscribe(x => {
                    m_selector.Select(m_selectIndex);
                })
                .AddTo(this);
                
        }

        private int CalculateIndex(InputSignal<Vector2> inputSignal) {
            return inputSignal
                .Value
                .GetDirectionIndex(m_selector.Spells.Count, 0.5f);
        } 
    }
}