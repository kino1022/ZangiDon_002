using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Slot.Interface;

namespace Src.Spell.Container.Sendable.Interface {
    public interface ISpellSendableManager<Manager,Instance,Slot> 
        where Manager : ISpellManager<Instance,Slot> 
        where Instance : ISpellInstance
        where Slot :　ISpellSlot<Instance>
    {
        bool Sendable { get; }
    }
}