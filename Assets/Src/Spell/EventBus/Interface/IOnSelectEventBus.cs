using Src.Spell.Instance.Interface;

namespace Src.Spell.EventBus.Interface {
    public interface IOnSelectEventBus {
        ISpellInstance Spell { get; }
    }
}