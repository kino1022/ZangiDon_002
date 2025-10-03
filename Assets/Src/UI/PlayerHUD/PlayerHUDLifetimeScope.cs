using System.ComponentModel;
using GeneralModule.Scope;
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
using Unity.Collections;
using VContainer;

namespace Src.UI.PlayerHUD {
    public class PlayerHUDLifetimeScope : ListableLifetimeScope {
        
        [OdinSerialize]
        [Sirenix.OdinInspector.ReadOnly]
        private ISpellManagerPresenter<IMainSpellManager,IMainSpellInstance,IMainSpellSlot> m_mainPresenter;
        
        [OdinSerialize]
        [Sirenix.OdinInspector.ReadOnly]
        private ISpellManagerPresenter<ISubSpellManager,ISubSpellInstance,ISubSpellSlot> m_subPresenter;

        [OdinSerialize]
        [Sirenix.OdinInspector.ReadOnly]
        private ISpellManagerPresenter<ISpellSelector, ISpellInstance, ISelectorSlot> m_selectorPresenter;

        public void Start() {
            m_mainPresenter = Container.Resolve<ISpellManagerPresenter<IMainSpellManager,IMainSpellInstance,IMainSpellSlot>>();
            m_subPresenter = Container.Resolve<ISpellManagerPresenter<ISubSpellManager,ISubSpellInstance,ISubSpellSlot>>();
            m_selectorPresenter = Container.Resolve<ISpellManagerPresenter<ISpellSelector,ISpellInstance,ISelectorSlot>>();
        }
        
    }
}