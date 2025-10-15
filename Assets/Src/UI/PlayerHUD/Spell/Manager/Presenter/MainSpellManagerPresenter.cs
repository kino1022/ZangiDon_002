using Src.Spell.Instance.Main.Interface;
using Src.Spell.Manager.Main.Interface;
using Src.Spell.Slot.Main.Interface;
using Src.UI.PlayerHUD.Spell.Manager.View.Interface;
using VContainer;

namespace Src.UI.PlayerHUD.Spell.Manager.Presenter {
    public class MainSpellManagerPresenter : ASpellManagerPresenter<IMainSpellManager,IMainSpellInstance,IMainSpellSlot> {

        [Inject]
        public MainSpellManagerPresenter(IMainSpellManager model,
            ISpellManagerView<IMainSpellInstance, IMainSpellSlot> view) : base(model, view) {
            
        }
    }
}