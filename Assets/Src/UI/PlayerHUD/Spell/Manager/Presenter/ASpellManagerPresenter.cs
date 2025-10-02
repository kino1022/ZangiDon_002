using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Slot.Interface;
using Src.UI.PlayerHUD.Spell.Manager.Presenter.Interface;
using Src.UI.PlayerHUD.Spell.Manager.View.Interface;
using Src.UI.PlayerHUD.Spell.Slot.Presenter;
using Src.UI.PlayerHUD.Spell.Slot.View;

namespace Src.UI.PlayerHUD.Spell.Manager.Presenter {
    public abstract class ASpellManagerPresenter<Manager,Instance,Slot> : ISpellManagerPresenter<Manager,Instance,Slot> 
        where Manager : ISpellManager<Instance, Slot>
        where Instance : ISpellInstance
        where Slot : ISpellSlot<Instance> 
    {
        
        protected Manager m_model;

        protected ISpellManagerView<Manager, Instance, Slot> m_view;

        private Dictionary<ISpellSlotPresenter, ISpellSlotView> m_slotDictionaries = new();

        public void Start() {
            CreateSlotPresenter();
            m_slotDictionaries
                .Select(x => x.Key)
                .ToList()
                .ForEach(x => Start());
        }

        public void Dispose() {
            
        }

        private void CreateSlotPresenter() {
            foreach (var pair in m_view.SlotViews) {
                var slot = m_model.Spells[pair.Key] ?? throw new NullReferenceException();
                var presenter = new SpellSlotPresenter<Instance>(slot,pair.Value);
                m_slotDictionaries.Add(presenter, pair.Value);
            }
        }
        
    }
}