using Src.Spell.CastAction.Interface;
using Src.Spell.EventBus.Interface;
using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Manager.Sub.Interface;
using Src.Spell.Slot.Sub.Interface;

namespace Src.Spell.Manager.Sub {
    public class SubSpellManager : ASelectedSpellManager<ISubSpellInstance,ISubSpellSlot>, ISubSpellManager {

        public void PreCast() {
            
            if (m_spells.Count is 0 || m_spells is null) return;
            
            foreach (var pair in Spells) {
                var spell = pair.Value;
                
                if (spell is null) {
                    continue;
                }

                var action = spell.Spell.CurrentValue.PreCast;
                
                action?.Action(gameObject, m_resolver);
            }
        }

        public void PostCast() {
            
            if (m_spells.Count is 0 || m_spells is null) return;
            
            foreach (var pair in Spells) {
                var spell = pair.Value;

                if (spell is null) {
                    continue;
                }
                
                var action = spell.Spell.CurrentValue.PostCast;

                action?.Action(gameObject, m_resolver);
            }
        }

        protected override void OnSelectSpellAdded(ISubSpellInstance spell) {
            base.OnSelectSpellAdded(spell);

            var action = spell.OnSelect;
            
            action?.Action(gameObject, m_resolver);
        }
    }
}