using Src.Spell.Data.Interface;
using Src.Spell.Instance.Factory.Interface;
using Src.Spell.Instance.Interface;

namespace Src.Spell.Instance.Factory {
    public abstract class ASpellFactory<Instance, Data> : ISpellInstanceFactory<Instance,Data> 
        where Instance : ISpellInstance
        where Data : ISpellData {
        public abstract Instance Create(Data data);
    }
}