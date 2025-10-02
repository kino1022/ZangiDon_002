using Src.Spell.Data.Main.Interface;
using Src.Spell.Instance.Factory.Interface;
using Src.Spell.Instance.Main.Interface;

namespace Src.Spell.Instance.Factory.Main.Interface {
    public interface IMainSpellInstanceFactory : ISpellInstanceFactory<IMainSpellInstance, IMainSpellData> {
        
    }
}