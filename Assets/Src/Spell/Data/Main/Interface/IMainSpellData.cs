using Src.Spell.Data.Interface;
using Src.Spell.CastAction.Interface;

namespace Src.Spell.Data.Main.Interface {
    public interface IMainSpellData : ISpellData {
        
        ICastAction OnCast { get; }
        
    }
}