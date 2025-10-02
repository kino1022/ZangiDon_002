using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Factory.Interface;
using Src.Spell.Slot.Interface;

namespace Src.Spell.Slot.Factory {
    public abstract class ASlotFactory<Instance, Slot> : ISlotFactory<Instance, Slot> 
        where Instance : ISpellInstance
        where Slot : ISpellSlot<Instance> {
        public abstract Slot Create();
    }
}