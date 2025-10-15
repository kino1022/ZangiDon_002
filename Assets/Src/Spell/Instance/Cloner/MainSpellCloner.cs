using System;
using R3;
using Src.Spell.Instance.Interface;
using Src.Spell.Instance.Main;

namespace Src.Spell.Instance.Cloner {
    public class MainSpellCloner : ISpellCloner<MainSpellInstance> {
        public MainSpellInstance Clone(ISpellInstance instance) {
            
            if (instance is null) throw new ArgumentNullException(nameof(instance));

            if (instance is MainSpellInstance clonebase) {
                MainSpellInstance result = clonebase with { };
                return result;
            }

            throw new ArgumentException();
        }
    }
}