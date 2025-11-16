using System.Collections.Generic;
using R3;
using Src.Control;
using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Selector.Interface;
using Src.Spell.Slot.Selector.Interface;
using Src.UI.PlayerHUD.Spell.Manager.View.Interface;
using Src.UI.PlayerHUD.Spell.Slot.View;
using UnityEngine;
using VContainer;

namespace Src.UI.PlayerHUD.Spell.Manager.Presenter {
    public class SpellSelectorPresenter : ASpellManagerPresenter<ISpellSelector, ISpellInstance, ISelectorSlot> {

        private readonly ISpellSelectAction _action;
        
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private float _zoomRatio = 1.5f;
        
        private readonly Dictionary<SpellSlotView, Vector3> _defaultScales = new Dictionary<SpellSlotView, Vector3>();

        [Inject]
        public SpellSelectorPresenter(ISpellSelector model, ISpellManagerView<ISpellInstance, ISelectorSlot> view, ISpellSelectAction action) : base(model, view) {
            _action = action;
            
            GetDefaultScales();
            
            RegisterIndexChanged();
        }

        private void RegisterIndexChanged() {
            Observable
                .EveryValueChanged(_action, x => x.SelectIndex)
                .Subscribe(x => {

                    if (x == -1) {
                        foreach (var slot in m_view.SlotViews.Values) {
                            if (slot is SpellSlotView spellSlotView) {
                                spellSlotView.transform.localScale = _defaultScales[spellSlotView];
                            }
                        }
                        return;
                    }

                    OnIndexChanged(x);
                })
                .AddTo(_disposables);
        }

        private void OnIndexChanged(int next) {
            var slot = m_view.SlotViews[next];
            
            foreach (var slotView in m_view.SlotViews.Values) {
                if (slotView is SpellSlotView spellSlotView) {
                    if (slotView == slot) {
                        spellSlotView.transform.localScale = _defaultScales[spellSlotView] * _zoomRatio;
                    }
                    else {
                        spellSlotView.transform.localScale = _defaultScales[spellSlotView];
                    }
                }
            }
        }
        
        
        private void GetDefaultScales() {
            foreach (var slot in m_view.SlotViews.Values) {
                if (slot is SpellSlotView spellSlotView) {
                    _defaultScales[spellSlotView] = spellSlotView.transform.localScale;
                }
            }
        }

        
    }
}