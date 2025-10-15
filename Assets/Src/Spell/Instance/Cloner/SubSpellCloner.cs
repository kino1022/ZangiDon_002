using System;
using Src.Spell.Instance.Interface;
using Src.Spell.Instance.Sub;
using Src.Spell.Instance.Sub.Interface;

namespace Src.Spell.Instance.Cloner {
    public class SubSpellCloner : ISpellCloner<SubSpellInstance> {

        public SubSpellInstance Clone(ISpellInstance instance) {
            
            if (instance is null) throw new ArgumentNullException(nameof(instance));
            
            if (instance is SubSpellInstance cloneBase) {
                SubSpellInstance result = cloneBase with { };
                return result;
            }

            throw new ArgumentException();
        }
    }
}