using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Data.Main.Interface;
using Src.Spell.Instance.Interface;
using Src.Spell.Instance.Main.Interface;
using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Manager.Main.Interface;
using Src.Spell.Manager.Selector.Interface;
using Src.Spell.Manager.Sub.Interface;
using Src.Spell.Slot.Main.Interface;
using Src.Spell.Slot.Selector.Interface;
using Src.Spell.Slot.Sub.Interface;
using Src.UI.PlayerHUD.Spell.Manager.Presenter;
using Src.UI.PlayerHUD.Spell.Manager.Presenter.Interface;
using Src.UI.PlayerHUD.Spell.Manager.View.Interface;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Spell.Installer.Objects {
    public class SpellUIInstaller : SerializedMonoBehaviour, IInstaller {

        [OdinSerialize]
        private ISpellManagerView<IMainSpellInstance, IMainSpellSlot> m_mainManagerView;
        
        [OdinSerialize]
        private ISpellManagerView<ISubSpellInstance, ISubSpellSlot> m_subManagerView;

        [OdinSerialize]
        private ISpellManagerView<ISpellInstance, ISelectorSlot> m_selectorView;

        public void Install(IContainerBuilder builder) {

            if (m_mainManagerView is not null) {
                builder
                    .RegisterComponent(m_mainManagerView)
                    .As<ISpellManagerView<IMainSpellInstance, IMainSpellSlot>>();
            }

            if (m_subManagerView is not null) {
                builder
                    .RegisterComponent(m_subManagerView)
                    .As<ISpellManagerView<ISubSpellInstance, ISubSpellSlot>>();
            }

            if (m_selectorView is not null) {
                builder
                    .RegisterComponent(m_selectorView)
                    .As<ISpellManagerView<ISpellInstance, ISelectorSlot>>();
            }

            builder
                .Register<ISpellManagerPresenter<IMainSpellManager, IMainSpellInstance, IMainSpellSlot>,MainSpellManagerPresenter>(Lifetime.Singleton)
                .As<ISpellManagerPresenter<IMainSpellManager, IMainSpellInstance, IMainSpellSlot>>();
            
            builder
                .Register<ISpellManagerPresenter<ISubSpellManager,ISubSpellInstance,ISubSpellSlot>,SubSpellManagerPresenter>(Lifetime.Singleton)
                .As<ISpellManagerPresenter<ISubSpellManager, ISubSpellInstance, ISubSpellSlot>>();
            
            builder
                .Register<ISpellManagerPresenter<ISpellSelector,ISpellInstance,ISelectorSlot>,SpellSelectorPresenter>(Lifetime.Singleton)
                .As<ISpellManagerPresenter<ISpellSelector,ISpellInstance,ISelectorSlot>>();

        }
    }
}