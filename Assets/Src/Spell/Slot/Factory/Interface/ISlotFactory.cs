using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Interface;

namespace Src.Spell.Slot.Factory.Interface {
    public interface ISlotFactory<Instance,Slot> where Instance : ISpellInstance where Slot : ISpellSlot<Instance> {
        Slot Create();
    }
}