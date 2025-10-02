using GeneralModule.Amount;
using Src.Spell.Data.Sub.Interface;
using Src.Spell.Instance.Factory.Sub.Interface;
using Src.Spell.Instance.Sub;
using Src.Spell.Instance.Sub.Interface;

namespace Src.Spell.Instance.Factory.Sub {
    public class SubSpellFactory : ASpellFactory<ISubSpellInstance, ISubSpellData>, ISubSpellInstanceFactory {
        public override ISubSpellInstance Create(ISubSpellData data) {
            return new SubSpellInstance(
                data.Sprite,
                new AmountModule(data.MaxAmount),
                data.OnSelect,
                data.PreCast,
                data.PostCast
            );
        }
    }
}