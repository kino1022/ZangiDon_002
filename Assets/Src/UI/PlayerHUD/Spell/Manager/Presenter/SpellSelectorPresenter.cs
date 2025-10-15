using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Selector.Interface;
using Src.Spell.Slot.Selector.Interface;
using Src.UI.PlayerHUD.Spell.Manager.View.Interface;
using VContainer;

namespace Src.UI.PlayerHUD.Spell.Manager.Presenter {
    public class SpellSelectorPresenter : ASpellManagerPresenter<ISpellSelector, ISpellInstance, ISelectorSlot> {
        
        [Inject]
        public SpellSelectorPresenter(ISpellSelector model, ISpellManagerView<ISpellInstance, ISelectorSlot> view) : base(model, view) {}
    }
}