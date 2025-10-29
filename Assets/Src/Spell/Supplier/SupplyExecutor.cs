using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Manager.Selector.Interface;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Spell.Supplier {

    public interface ISupplyExecutor {
        
    }

    public class SupplyExecutor : SerializedMonoBehaviour, ISupplyExecutor {

        [Title("設定")]
        
        [SerializeField]
        [LabelText("補充感覚")]
        private float m_supplyInterval = 5.0f;

        [Title("ランタイム")]
        
        [SerializeField]
        [LabelText("補充待機中か")]
        [ReadOnly]
        private bool m_isWaiting = false;
        
        [Title("参照")]

        [OdinSerialize]
        [LabelText("スペルセレクター")]
        [ReadOnly]
        private ISpellSelector m_selector;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_selector = m_resolver.Resolve<ISpellSelector>();
            
            CreateObserveStream();
        }

        private void CreateObserveStream() {
            Observable
                .EveryUpdate()
                .Subscribe(_ => {
                    
                    if (m_isWaiting is true) {
                        return;
                    }

                    var isEmpty = false;
                    
                    m_selector.Spells.Values.ToList().ForEach(x => {
                        if (x.IsEmpty is true) isEmpty = true;
                    });

                    if (isEmpty) {
                        WaitSpellSupply().Forget();
                    }
                    
                })
                .AddTo(this);
        }

        private async UniTask WaitSpellSupply() {
            try {
                m_isWaiting = true;
                await UniTask.Delay(TimeSpan.FromSeconds(m_supplyInterval));
                m_selector.Supply();
            }
            catch (OperationCanceledException) {

            }
            finally {
                m_isWaiting = false;
            }
        }
    }
}