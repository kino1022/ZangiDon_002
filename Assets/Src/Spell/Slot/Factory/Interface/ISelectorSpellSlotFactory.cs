using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Interface;
using Src.Spell.Slot.Selector.Interface;

namespace Src.Spell.Slot.Factory.Interface {
    public interface ISelectorSpellSlotFactory : ISlotFactory<ISpellInstance, ISelectorSlot> {
        
    }
}