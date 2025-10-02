using Src.Spell.Data.Interface;
using Src.Spell.Instance.Interface;

namespace Src.Spell.Instance.Factory.Interface {
    public interface ISpellInstanceFactory<Instance, Data> 
        where Instance : ISpellInstance
        where Data : ISpellData 
    {
        Instance Create(Data data);
    }
}