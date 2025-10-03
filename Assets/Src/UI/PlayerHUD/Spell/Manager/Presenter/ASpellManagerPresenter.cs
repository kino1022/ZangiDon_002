using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Manager.Selector.Interface;
using Src.Spell.Slot.Interface;
using Src.UI.PlayerHUD.Spell.Manager.Presenter.Interface;
using Src.UI.PlayerHUD.Spell.Manager.View.Interface;
using Src.UI.PlayerHUD.Spell.Slot.Presenter;
using Src.UI.PlayerHUD.Spell.Slot.View;
using UnityEngine;
using VContainer;

namespace Src.UI.PlayerHUD.Spell.Manager.Presenter {
    public abstract class ASpellManagerPresenter<Manager,Instance,Slot> : ISpellManagerPresenter<Manager,Instance,Slot> 
        where Manager : ISpellManager<Instance, Slot>
        where Instance : ISpellInstance
        where Slot : ISpellSlot<Instance> 
    {
        
        protected Manager m_model;

        protected ISpellManagerView<Instance,Slot> m_view;

        private Dictionary<ISpellSlotPresenter, ISpellSlotView> m_slotDictionaries = new();

        [Inject]
        public virtual void Construct(Manager model, ISpellManagerView<Instance, Slot> view) {
            m_model = model;
            m_view = view;
        }

        public void Start() {
            CreateSlotPresenter();
            m_slotDictionaries
                .Select(x => x.Key)
                .ToList()
                .ForEach(x => Start());
            
            //矛盾があった場合は強制狩猟
            Debug.Assert(!CheckIntegrateView());
        }

        private bool CheckIntegrateView() {
            if (m_view.SlotViews.Count != m_model.Length || m_model.Spells.Count != m_view.SlotViews.Count) {
                Debug.LogWarning($"{m_model.GetType().Name}の管理量と{m_view.GetType().Name}のSlotViewの量が異なります");
                return false;
            }
            
            return true;
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