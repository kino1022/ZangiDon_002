using Src.Spell.Container.Sendable.Interface;
using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Manager.Sub.Interface;
using Src.Spell.Slot.Sub.Interface;
using VContainer;

namespace Src.Spell.Container.Sendable {
    public class SubSendableManager : ASendableModule<ISubSpellManager,ISubSpellInstance,ISubSpellSlot> , ISubSendableManager {
        
        [Inject]
        public SubSendableManager(ISubSpellManager manager) : base(manager) { }
        
    }
}