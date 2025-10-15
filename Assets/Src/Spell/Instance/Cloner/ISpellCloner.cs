using Src.Spell.Instance.Interface;

namespace Src.Spell.Instance.Cloner {
    public interface ISpellCloner<Instance> where Instance :class, ISpellInstance{

        Instance Clone(ISpellInstance instance);
    }
}