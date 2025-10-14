using Src.Spell.Data.Interface;
using Src.Spell.CastAction.Interface;

namespace Src.Spell.Data.Sub.Interface {
    
    public interface ISubSpellData : ISpellData {
        
        IOnSelectAction OnSelect { get; }
        
        IPreCastAction PreCast { get; }
        
        IPostCastAction PostCast { get; }
    }
}