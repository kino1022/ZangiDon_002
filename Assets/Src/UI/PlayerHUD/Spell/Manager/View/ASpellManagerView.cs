using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Interface;
using Src.UI.PlayerHUD.Spell.Manager.View.Interface;
using Src.UI.PlayerHUD.Spell.Slot.View;

namespace Src.UI.PlayerHUD.Spell.Manager.View {
    public class ASpellManagerView<Instance,Slot> : SerializedMonoBehaviour, ISpellManagerView<Instance,Slot> 
        where Instance : ISpellInstance
        where Slot : ISpellSlot<Instance>
    {
        [OdinSerialize]
        [LabelText("スペル表示")]
        protected Dictionary<int, ISpellSlotView> m_slotViews = new Dictionary<int, ISpellSlotView>();
        
        public Dictionary<int, ISpellSlotView> SlotViews => m_slotViews;
        
    } 
}