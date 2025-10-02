using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Manager.Sub.Interface;
using Src.Spell.Slot.Sub.Interface;

namespace Src.Spell.Container.Sendable.Interface {
    public interface ISubSendableManager : ISpellSendableManager<ISubSpellManager,ISubSpellInstance,ISubSpellSlot> {
        
    }
}