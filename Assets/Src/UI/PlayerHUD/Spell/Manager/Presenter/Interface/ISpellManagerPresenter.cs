using System;
using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Slot.Interface;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Spell.Manager.Presenter.Interface {
    public interface ISpellManagerPresenter<Manager,Instance,Slot> : IStartable , IDisposable 
        where Manager : ISpellManager<Instance,Slot>
        where Instance : ISpellInstance
        where Slot : ISpellSlot<Instance>
    {
        
    }
}