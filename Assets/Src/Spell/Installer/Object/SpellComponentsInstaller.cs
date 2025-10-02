using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Container.Sendable;
using Src.Spell.Container.Sendable.Interface;
using Src.Spell.Manager.Main.Interface;
using Src.Spell.Manager.Selector.Interface;
using Src.Spell.Manager.Sub.Interface;
using Src.Spell.Supplier;
using Src.Spell.Supplier.Interface;
using Src.Spell.Supplier.Pattern.Interface;
using VContainer;
using VContainer.Unity;

namespace Src.Spell.Installer.Object {
    public class SpellComponentsInstaller : SerializedMonoBehaviour , IInstaller {

        [OdinSerialize] 
        private ISpellSupplyPattern m_pattern;

        public void Install(IContainerBuilder builder) {
            
            builder
                .RegisterComponent(Utility.ComponentsUtility.GetComponentsFromWhole<IMainSpellManager>(gameObject))
                .As<IMainSpellManager>();
            
            builder
                .RegisterComponent(Utility.ComponentsUtility.GetComponentsFromWhole<ISubSpellManager>(gameObject))
                .As<ISubSpellManager>();
            
            builder
                .RegisterComponent(Utility.ComponentsUtility.GetComponentsFromWhole<ISpellSelector>(gameObject))
                .As<ISpellSelector>();
            
            builder
                .RegisterInstance(m_pattern)
                .As<ISpellSupplyPattern>();
            
            builder
                .Register<SpellSupplier>(Lifetime.Transient)
                .As<ISpellSupplier>();

            builder
                .Register<MainSendableManager>(Lifetime.Transient)
                .As<IMainSendableManager>();
            
            builder
                .Register<SubSendableManager>(Lifetime.Transient)
                .As<ISubSendableManager>();
            
        }
    }
}