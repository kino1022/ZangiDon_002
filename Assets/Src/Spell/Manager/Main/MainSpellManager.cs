using Src.Spell.Instance.Main.Interface;
using Src.Spell.Manager.Main.Interface;
using Src.Spell.Slot.Main.Interface;

namespace Src.Spell.Manager.Main {
    public class MainSpellManager : ASpellManager<IMainSpellInstance,IMainSpellSlot>, IMainSpellManager {

        public void OnCast() {
            
        }
    }
}