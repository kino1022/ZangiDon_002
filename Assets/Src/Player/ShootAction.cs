using System;
using R3;
using RinaBullet.Context.Container;
using RinaInput.Controller.Module;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Player.Interface;
using Src.Spell.Manager;
using Src.Spell.Manager.Main.Interface;
using Src.Spell.Manager.Sub.Interface;
using UnityEngine;
using VContainer;

namespace Src.Player {
    public class ShootAction : SerializedMonoBehaviour, IShootAction {
        
        private IObjectResolver m_resolver;

        [Title("参照")]
        
        [OdinSerialize]
        [LabelText("入力トリガー")]
        private IInputModule<float> m_inputModule;
        
        [OdinSerialize]
        [LabelText("サブスペル")]
        [ReadOnly]
        private ISubSpellManager m_subSpell;

        [OdinSerialize]
        [LabelText("メインスペル")]
        [ReadOnly]
        private IMainSpellManager m_mainSpell;
        
        [OdinSerialize]
        [LabelText("コンテキストコンテナ")]
        [ReadOnly]
        private IContextContainer m_contextContainer;


        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentException();
        }

        private void Start() {
            
            m_mainSpell = m_resolver.Resolve<IMainSpellManager>() 
                          ?? throw new NullReferenceException();
            
            m_subSpell = m_resolver.Resolve<ISubSpellManager>() 
                         ?? throw new NullReferenceException();
            
            m_contextContainer = m_resolver.Resolve<IContextContainer>()
                                 ?? throw new NullReferenceException();
            
            RegisterModuleInput();
            
        }

        [Button("使用")]
        public void Cast() {

            if (m_mainSpell.IsFull() is false) {
                Debug.Log("メインのスペルが存在しなかったために、スペルの使用処理を中断します");
                return;
            }
            
            m_subSpell.PreCast();
            
            m_mainSpell.OnCast();
            
            m_subSpell.PostCast();
            
            m_mainSpell.DecreaseAmount(1);
            
            m_subSpell.DecreaseAmount(1);
            
            m_contextContainer.Clear();
            
        }

        private void RegisterModuleInput() {
            m_inputModule
                .Stream
                .Subscribe(_ => {
                    Cast();
                })
                .AddTo(this);
        }
        
    }
}