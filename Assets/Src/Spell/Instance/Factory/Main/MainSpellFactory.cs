using GeneralModule.Amount;
using Src.Spell.Data.Main.Interface;
using Src.Spell.Instance.Factory.Main.Interface;
using Src.Spell.Instance.Main;
using Src.Spell.Instance.Main.Interface;
using UnityEngine;

namespace Src.Spell.Instance.Factory.Main {
    public class MainSpellFactory : ASpellFactory<IMainSpellInstance, IMainSpellData>, IMainSpellInstanceFactory {
        public override IMainSpellInstance Create(IMainSpellData data) {
            return new MainSpellInstance(
                data.OnCast,
                data.Sprite,
                new AmountModule(data.MaxAmount)
            );
        }
    }
}