using System;
using Cysharp.Threading.Tasks;
using ObservableCollections;
using R3;
using Sirenix.OdinInspector;
using Src.Spell.Manager.Selector.Interface;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Spell.Manager.Selector {
    [Serializable]
    public class SpellSupplyCounter : ISpellSupplyCounter, IStartable, IDisposable {

        [SerializeField]
        [LabelText("補充までの待機時間")]
        private float m_supplyInterval = 20.0f;

        [SerializeField]
        [LabelText("カウント中かどうか")]
        private bool m_isProcessing = false;
        
        
        private ISpellSelector m_selector;
        
        private CompositeDisposable m_disposable = new CompositeDisposable();
        
        private IObjectResolver m_resolver;

        [Inject]
        public SpellSupplyCounter(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public void Start() {
            m_selector = m_resolver.Resolve<ISpellSelector>() ?? throw new ArgumentNullException(nameof(m_resolver));
            
            RegisterEmptySpells();
        }

        public void Dispose() {
            m_disposable?.Dispose();
        }

        private void RegisterEmptySpells() {
            
            m_disposable = new CompositeDisposable();

            m_selector.Spells
                .ObserveChanged()
                .Subscribe(x => {
                    
                    //カウント中の変化は無視
                    if (!m_isProcessing) return;

                    //空があるならカウント処理
                    if (m_selector.IsEmpty()) {
                        m_isProcessing = true;
                        AwaitInterval().Forget();
                    }
                    
                })
                .AddTo(m_disposable);
        }

        private async UniTask AwaitInterval() {
            await UniTask.Delay(TimeSpan.FromSeconds(m_supplyInterval));
            
            m_selector.Supply();
            m_isProcessing = false;
        }
    }
}