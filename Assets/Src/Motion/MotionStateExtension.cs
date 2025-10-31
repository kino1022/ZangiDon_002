using System;
using Src.Utility;
using UnityEngine;
using VContainer.Unity;
using VContainer;

namespace Src.Motion {
    public static class MotionStateExtension {

        public static float GetFrameRate(this Animator animator, int layerIndex) {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(layerIndex);

            if (clipInfo.Length > 0) {
                AnimationClip clip = clipInfo[0].clip;
                return clip.frameRate;
            }
            
            return 0;
        }

        public static int GetCurrentFrame(this AnimatorStateInfo info, float frameRate) {
            // Enterでフレームレートが取得できていなければ何もしない
            if (frameRate <= 0) return 0;

            // 1. normalizedTimeから現在の再生時間（秒）を計算
            //    (stateInfo.length は OnStateEnter 時点では 0 のことがあるため、
            //     Updateで都度参照するのが安全です)
            float currentTimeInLoop = (info.normalizedTime % 1.0f) * info.length;

            // 2. 現在のフレームを計算
            return Mathf.FloorToInt(currentTimeInLoop * frameRate);
        }
        
        public static T GetComponentFromContainer<T>(this Animator animator) {
            var container = ComponentsUtility.GetComponentFromWhole<LifetimeScope>(animator.gameObject) ??
                            throw new ArgumentNullException();

            return container.Container.Resolve<T>() ?? throw new NullReferenceException();
        }
    }
}