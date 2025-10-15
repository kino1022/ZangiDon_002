using Src.Spell.Instance.Main.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Slot.Main.Interface;

namespace Src.Spell.Manager.Main.Interface {
    
    public interface IMainSpellManager : ISpellManager<IMainSpellInstance,IMainSpellSlot> {

        void OnCast();
        
    }
    
}