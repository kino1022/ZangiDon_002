using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Slot.Sub.Interface;

namespace Src.Spell.Manager.Sub.Interface {
    public interface ISubSpellManager : ISpellManager<ISubSpellInstance,ISubSpellSlot> {
        
    }
}