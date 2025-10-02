using System;
using Sirenix.Serialization;
using Src.Spell.Data.Main.Interface;
using Src.Spell.Data.Sub.Interface;
using Src.Spell.Instance.Factory.Interface;
using Src.Spell.Instance.Factory.Main.Interface;
using Src.Spell.Instance.Factory.Sub.Interface;
using Src.Spell.Instance.Interface;
using Src.Spell.Instance.Main.Interface;
using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Slot.Factory;
using Src.Spell.Slot.Factory.Interface;
using Src.Spell.Slot.Main.Interface;
using Src.Spell.Slot.Selector.Interface;
using Src.Spell.Slot.Sub.Interface;
using VContainer;
using VContainer.Unity;

namespace Src.Spell.Installer {
    /// <summary>
    /// GameManager.LifetimeScope向けにスペルシステムの運用に必要なオブジェクトを登録するためのIInstaller
    /// </summary>
    [Serializable]
    public class SpellSystemInstaller : IInstaller {

        [OdinSerialize] 
        private IMainSpellInstanceFactory m_mainInstanceFactory;
        
        [OdinSerialize] 
        private ISubSpellInstanceFactory m_subInstanceFactory;

        public void Install(IContainerBuilder builder) {

            builder
                .Register<IMainSpellSlotFactory, MainSpellSlotFactory>(Lifetime.Singleton)
                .As<ISlotFactory<IMainSpellInstance, IMainSpellSlot>>();

            builder
                .Register<ISubSpellSlotFactory, SubSpellSlotFactory>(lifetime: Lifetime.Singleton)
                .As<ISlotFactory<ISubSpellInstance, ISubSpellSlot>>();

            builder
                .Register<ISelectorSpellSlotFactory, SelectorSpellSlotFactory>(Lifetime.Singleton)
                .As<ISlotFactory<ISpellInstance, ISelectorSlot>>();

            builder
                .RegisterInstance(m_mainInstanceFactory)
                .As<ISpellInstanceFactory<IMainSpellInstance, IMainSpellData>>()
                .As<IMainSpellInstanceFactory>();

            builder
                .RegisterInstance(m_subInstanceFactory)
                .As<ISpellInstanceFactory<ISubSpellInstance, ISubSpellData>>()
                .As<ISubSpellInstanceFactory>();

        }
    }
}