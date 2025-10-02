using Src.Spell.Instance.Main.Interface;
using Src.Spell.Manager.Main.Interface;
using Src.Spell.Slot.Main.Interface;

namespace Src.Spell.Container.Sendable.Interface {
    public interface IMainSendableManager : ISpellSendableManager<IMainSpellManager,IMainSpellInstance,IMainSpellSlot> {
        
    }
}