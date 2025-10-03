using System.Collections.Generic;
using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Slot.Interface;
using Src.UI.PlayerHUD.Spell.Slot.View;

namespace Src.UI.PlayerHUD.Spell.Manager.View.Interface {
    public interface ISpellManagerView<Instance, Slot>
        where Instance : ISpellInstance
        where Slot : ISpellSlot<Instance> 
    {
        Dictionary<int, ISpellSlotView> SlotViews { get; }
    }
}