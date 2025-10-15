using System;
using RinaBullet.Context.Container;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Player.Interface;
using Src.Spell.Manager.Main.Interface;
using Src.Spell.Manager.Sub.Interface;
using VContainer;

namespace Src.Player {
    public class CastCallBackExecutor : SerializedMonoBehaviour, ICastCallBackExecutor {
        
        private IObjectResolver m_resolver;

        [Title("参照")]
        
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
            
        }

        [Button("使用")]
        public void Cast() {
            
            m_subSpell.PreCast();
            
            m_mainSpell.OnCast();
            
            m_contextContainer.Clear();
            
            m_subSpell.PostCast();
            
        }
        
    }
}