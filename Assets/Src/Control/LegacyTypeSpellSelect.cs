using System.Collections.Generic;
using R3;
using RinaInput.Controller.Module;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Manager.Selector.Interface;
using VContainer;

namespace Src.Control {
    public class LegacyTypeSpellSelect : SerializedMonoBehaviour {

        [Title("入力モジュール")] 
        
        [OdinSerialize]
        [LabelText("インデックスと選択キー")]
        private Dictionary<int, IInputModule<float>> m_modules;
        
        [Title("参照")]
        
        [OdinSerialize]
        [ReadOnly]
        private ISpellSelector m_selector;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_selector = m_resolver.Resolve<ISpellSelector>();

            foreach (var module in m_modules) {
                RegisterSelectInput(module);
            }
        }

        private void RegisterSelectInput(KeyValuePair<int, IInputModule<float>> pair) {
            pair
                .Value
                .Stream
                .Subscribe(_ => {
                    m_selector.Select(pair.Key);
                })
                .AddTo(this);
        }
    }
}