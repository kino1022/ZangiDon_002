using Src.Spell.Data.Sub.Interface;
using Src.Spell.Instance.Factory.Sub.Interface;
using Src.Spell.Instance.Main.Interface;
using Src.Spell.Instance.Sub;
using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Slot.Factory.Interface;
using Src.Spell.Slot.Main.Interface;
using Src.Spell.Slot.Sub;
using Src.Spell.Slot.Sub.Interface;

namespace Src.Spell.Slot.Factory {
    public class SubSpellSlotFactory : ASlotFactory<ISubSpellInstance,ISubSpellSlot>, ISubSpellSlotFactory {
        public override ISubSpellSlot Create() {
            return new SubSpellSlot();
        }
    }
}