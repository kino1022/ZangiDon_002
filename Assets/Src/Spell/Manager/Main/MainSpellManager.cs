using Src.Spell.Instance.Main.Interface;
using Src.Spell.Manager.Main.Interface;
using Src.Spell.Slot.Main.Interface;

namespace Src.Spell.Manager.Main {
    public class MainSpellManager : ASelectedSpellManager<IMainSpellInstance,IMainSpellSlot>, IMainSpellManager {

        public void OnCast() {
            
            if (this.IsEmpty()) return;
            
            foreach (var spell in Spells) {
                var instance = spell.Value.Spell.CurrentValue;
                
                if (instance is null) {
                    continue;
                }
                
                instance.OnCast?.Action(gameObject, m_resolver);
            }
        }
    }
}