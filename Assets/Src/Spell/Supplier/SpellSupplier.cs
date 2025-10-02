using System;
using Sirenix.OdinInspector;
using Src.Spell.Data;
using Src.Spell.Data.Interface;
using Src.Spell.Data.Main.Interface;
using Src.Spell.Data.Sub.Interface;
using Src.Spell.Instance.Factory.Main.Interface;
using Src.Spell.Instance.Factory.Sub.Interface;
using Src.Spell.Instance.Interface;
using Src.Spell.Supplier.Interface;
using Src.Spell.Supplier.Pattern.Interface;
using UnityEngine;
using VContainer;

namespace Src.Spell.Supplier {
    [Serializable]
    public class SpellSupplier : ISpellSupplier {

        private IMainSpellInstanceFactory m_mainFactory;
        
        private ISubSpellInstanceFactory m_subFactory;

        private ISpellSupplyPattern m_pattern;

        [Inject]
        public SpellSupplier(IObjectResolver resolver) {
            
            m_mainFactory = resolver.Resolve<IMainSpellInstanceFactory>();
            
            m_subFactory = resolver.Resolve<ISubSpellInstanceFactory>();
            
            m_pattern = resolver.Resolve<ISpellSupplyPattern>();
            
        }

        public ISpellInstance Supply() {
            var basedata = m_pattern.Lottery();
            var data = CreateRuntimeData(basedata);
            
            if (data is IMainSpellData main) {
                return m_mainFactory.Create(main);
            }

            if (data is ISubSpellData sub) {
                return m_subFactory.Create(sub);
            }
            
            return null;
        }

        private ISpellData CreateRuntimeData(ASpellData data) {

            if (data is null) {
                throw new ArgumentNullException();
            }
            
            return ScriptableObject.Instantiate(data);
        }
    }
}