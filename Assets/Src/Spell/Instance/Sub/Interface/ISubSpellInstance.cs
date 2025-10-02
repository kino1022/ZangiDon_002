using Src.Spell.Instance.CastAction.Interface;
using Src.Spell.Instance.Interface;

namespace Src.Spell.Instance.Sub.Interface {
    /// <summary>
    /// サブのスペルに対して約束するインタフェース
    /// </summary>
    public interface ISubSpellInstance : ISpellInstance {
        
        /// <summary>
        /// メインの効果の発動直前に対して発動する効果
        /// </summary>
        IPreCastAction PreCast { get; init; }
        
        /// <summary>
        /// メインの効果の発動直後に対して発動する効果
        /// </summary>
        IPostCastAction PostCast { get; init; }
        
        /// <summary>
        /// スペルとして選択された際に発動する効果
        /// </summary>
        IOnSelectAction OnSelect { get; init; }
        
    }
}