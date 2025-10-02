using Src.Spell.Data.Sub.Interface;
using Src.Spell.Instance.Factory.Interface;
using Src.Spell.Instance.Sub.Interface;

namespace Src.Spell.Instance.Factory.Sub.Interface {
    public interface ISubSpellInstanceFactory : ISpellInstanceFactory<ISubSpellInstance, ISubSpellData> {
        
    }
}