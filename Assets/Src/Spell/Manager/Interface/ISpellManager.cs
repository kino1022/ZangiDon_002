using ObservableCollections;
using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Interface;

namespace Src.Spell.Manager.Interface {
    public interface ISpellManager<Instance,Slot> where Instance : ISpellInstance  where Slot : ISpellSlot<Instance>{
        
        IReadOnlyObservableDictionary<int,Slot> Spells { get; }
        
        int Length { get; }
        
        void Add (Instance spell);
        
        void Remove (int index);
        
    }
}