using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Factory.Interface;
using Src.Spell.Slot.Selector;
using Src.Spell.Slot.Selector.Interface;

namespace Src.Spell.Slot.Factory {
    public class SelectorSpellSlotFactory : ASlotFactory<ISpellInstance, ISelectorSlot> , ISelectorSpellSlotFactory{
        public override ISelectorSlot Create() {
            return new SelectorSpellSlot();
        }
    }
}