using UnityEngine;
using VContainer;

namespace Src.Spell.CastAction.Interface {
    public interface IAction {
        
        void Action(GameObject caster, IObjectResolver resolver);
    }
}