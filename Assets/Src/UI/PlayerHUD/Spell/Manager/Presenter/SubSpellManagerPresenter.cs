using Src.Spell.Instance.Sub.Interface;
using Src.Spell.Manager.Sub.Interface;
using Src.Spell.Slot.Sub.Interface;
using Src.UI.PlayerHUD.Spell.Manager.View.Interface;
using VContainer;

namespace Src.UI.PlayerHUD.Spell.Manager.Presenter {
    public class SubSpellManagerPresenter : ASpellManagerPresenter<ISubSpellManager,ISubSpellInstance,ISubSpellSlot> {
        
        [Inject]
        public SubSpellManagerPresenter(ISubSpellManager model, ISpellManagerView<ISubSpellInstance, ISubSpellSlot> view) : base(model, view) {}
    }
}