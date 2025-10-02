using System;
using Src.Spell.Container.Sendable.Interface;
using Src.Spell.Instance.Main.Interface;
using Src.Spell.Manager.Main.Interface;
using Src.Spell.Slot.Main.Interface;
using VContainer;

namespace Src.Spell.Container.Sendable {
    
    public class MainSendableManager : ASendableModule<IMainSpellManager,IMainSpellInstance,IMainSpellSlot>, IMainSendableManager {

        [Inject]
        public MainSendableManager(IMainSpellManager manager) : base(manager) { }
    }
}