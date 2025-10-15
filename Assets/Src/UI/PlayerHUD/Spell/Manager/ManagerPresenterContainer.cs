using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Instance.Interface;
using Src.Spell.Instance.Main.Interface;
using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Manager.Main.Interface;
using Src.Spell.Manager.Selector.Interface;
using Src.Spell.Manager.Sub.Interface;
using Src.Spell.Slot.Main.Interface;
using Src.Spell.Slot.Selector.Interface;
using Src.Spell.Slot.Sub.Interface;
using Src.UI.PlayerHUD.Spell.Manager.Presenter.Interface;
using UnityEngine;
using VContainer;

namespace Src.UI.PlayerHUD.Spell.Manager {
    [DefaultExecutionOrder(1000)]
    public class ManagerPresenterContainer : SerializedMonoBehaviour {

        private IObjectResolver m_resolver;
        
        [Title("メイン")]
        [OdinSerialize]
        private ISpellManagerPresenter<IMainSpellManager,IMainSpellInstance,IMainSpellSlot> m_mainPresenter;
        
        [Title("サブ")]
        [OdinSerialize]
        private ISpellManagerPresenter<ISubSpellManager, ISubSpellInstance, ISubSpellSlot> m_subPresenter;
        
        [Title("セレクター")]
        [OdinSerialize]
        private ISpellManagerPresenter<ISpellSelector, ISpellInstance, ISelectorSlot> m_selectorPresenter;
        
        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException();
        }

        private void Start() {

            m_mainPresenter = m_resolver
                .Resolve<ISpellManagerPresenter<IMainSpellManager, IMainSpellInstance, IMainSpellSlot>>()
                ?? throw new ArgumentNullException();

            m_subPresenter = m_resolver
                .Resolve<ISpellManagerPresenter<ISubSpellManager, ISubSpellInstance, ISubSpellSlot>>()
                ?? throw new ArgumentNullException();

            m_selectorPresenter = m_resolver
                .Resolve<ISpellManagerPresenter<ISpellSelector, ISpellInstance, ISelectorSlot>>()
                ?? throw new ArgumentNullException();
            
            m_mainPresenter.Start();
            
            m_subPresenter.Start();
            
            m_selectorPresenter.Start();
        }
    }
}