using Sirenix.OdinInspector;
using Src.Move;
using Src.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Motion {
    /// <summary>
    /// アタッチしたアニメーションの動作中は自由落下が起こらなくなる
    /// </summary>
    [InfoBox("アタッチしたアニメーションの動作中は自由落下が起こらなくなる")]
    public class CancelFreeFall : SerializedStateMachineBehaviour {
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            
            var container = ComponentsUtility.GetComponentFromWhole<LifetimeScope>(animator.gameObject);

            var fallManager = container.Container.Resolve<IFreeFallManager>() ?? throw new MissingComponentException("Container");
            
            fallManager.SetEnabled(false);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            
            var container = ComponentsUtility.GetComponentFromWhole<LifetimeScope>(animator.gameObject);
            
            var fallManager = container.Container.Resolve<IFreeFallManager>() ?? throw new MissingComponentException("Container");
            
            fallManager.SetEnabled(true);
        }
        
    }
}