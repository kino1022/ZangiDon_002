using Src.Spell.Instance.Interface;
using Src.Spell.Instance.Main.Interface;
using Src.Spell.Instance.Sub.Interface;

namespace Src.Spell.Supplier.Interface {
    
    public interface ISpellSupplier {

        ISpellInstance Supply();

    }
}