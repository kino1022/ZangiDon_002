using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Src.Motion.Clip {

    public interface IAnimationAction {

        void Execute(Animator animator);
        
    }

    public interface IAnimationAction<ValueType> where ValueType : struct {
        
        void Execute(Animator animator, ValueType value);
        
    }
    
    [CreateAssetMenu(menuName = "Project/Motion/Action")]
    public class AnimationAction : SerializedScriptableObject, IAnimationAction {

        [SerializeField] [LabelText("アニメーションの名前")] [InfoBox("何度でも言いますが文字列認証はクソです、考えたやつはバカです")]
        private string m_animationName;

        [SerializeField]
        [LabelText("ハッシュ値")]
        [ReadOnly]
        private int m_hashCode = 0;

        private void OnEnable() {
            m_hashCode = Animator.StringToHash(m_animationName);
        }

        public void Execute(Animator animator) {
            if (animator == null) throw new NullReferenceException("animator is null");
            
            animator.SetTrigger(m_hashCode);
        }
        
    }

    [CreateAssetMenu(menuName = "Project/Motion/Action(float)")]
    public class AnimationFloatParameter : SerializedScriptableObject, IAnimationAction<float>  {
        
        [SerializeField] [LabelText("アニメーションの名前")] [InfoBox("何度でも言いますが文字列認証はクソです、考えたやつはバカです")]
        private string m_animationName;

        [SerializeField]
        [LabelText("ハッシュ値")]
        [ReadOnly]
        private int m_hashCode = 0;

        private void OnEnable() {
            m_hashCode = Animator.StringToHash(m_animationName);
        }

        public void Execute(Animator animator, float value) {
            
            if (animator == null) throw new NullReferenceException("animator is null");
            
            animator.SetFloat(m_hashCode, value);
        }

    }

    [CreateAssetMenu(menuName = "Project/Motion/Action(int)")]
    public class AnimationIntParameter : SerializedScriptableObject, IAnimationAction<int> {
        [SerializeField] [LabelText("アニメーションの名前")] [InfoBox("何度でも言いますが文字列認証はクソです、考えたやつはバカです")]
        private string m_animationName;

        [SerializeField]
        [LabelText("ハッシュ値")]
        [ReadOnly]
        private int m_hashCode = 0;

        private void OnEnable() {
            m_hashCode = Animator.StringToHash(m_animationName);
        }

        public void Execute(Animator animator, int value) {
            
            if (animator == null) throw new NullReferenceException("animator is null");
            
            animator.SetInteger(m_hashCode, value);
        }
    }
    
    
    [CreateAssetMenu(menuName = "Project/Motion/Action(bool)")]
    public class AnimationBoolParameter : SerializedScriptableObject, IAnimationAction<bool> {
        [SerializeField] 
        [LabelText("アニメーションの名前")]
        [InfoBox("何度でも言いますが文字列認証はクソです、考えたやつはバカです")]
        private string m_animationName;

        [SerializeField]
        [LabelText("ハッシュ値")]
        [ReadOnly]
        private int m_hashCode = 0;

        private void OnEnable() {
            m_hashCode = Animator.StringToHash(m_animationName);
        }

        public void Execute(Animator animator, bool value) {
            
            if (animator == null) throw new NullReferenceException("animator is null");
            
            animator.SetBool(m_hashCode, value);
        }
    }
}