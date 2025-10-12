using System;
using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Selector.Interface;

namespace Src.Spell.Slot.Selector {
    [Serializable]
    public class SelectorSpellSlot : ASpellSlot<ISpellInstance>, ISelectorSlot {
        
    }
}