using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Slot.Selector.Interface;

namespace Src.Spell.Manager.Selector.Interface {
    
    public interface ISpellSelector : ISpellManager<ISpellInstance,ISelectorSlot> {

        void Select(int index);

        void Supply();
    }
}