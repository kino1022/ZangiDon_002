using System;
using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Slot.Sub.Interface;

namespace Src.Spell.Slot.Sub {
    [Serializable]
    public class SubSpellSlot : ASpellSlot<ISubSpellInstance> , ISubSpellSlot {

    }
}