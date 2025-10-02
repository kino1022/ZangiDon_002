using System;
using Src.Spell.Instance.Main.Interface;
using Src.Spell.Slot.Factory.Interface;
using Src.Spell.Slot.Main;
using Src.Spell.Slot.Main.Interface;

namespace Src.Spell.Slot.Factory {
    
    public class MainSpellSlotFactory : ASlotFactory<IMainSpellInstance,IMainSpellSlot> , IMainSpellSlotFactory {

        public override IMainSpellSlot Create() {
            return new MainSpellSlot();
        }
        
    }
}