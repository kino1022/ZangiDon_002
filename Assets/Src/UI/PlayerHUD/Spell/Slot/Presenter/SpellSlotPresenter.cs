using System;
using R3;
using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Interface;
using Src.UI.PlayerHUD.Spell.Slot.View;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Spell.Slot.Presenter {
    /// <summary>
    /// スペルスロットの変化を観測するクラスに対して約束するインタフェース
    /// </summary>
    /// <typeparam name="Instance"></typeparam>
    [Serializable]
    public class SpellSlotPresenter<Instance> : ISpellSlotPresenter, IStartable where Instance : ISpellInstance {
        
        
        /// <summary>
        /// 大型改修が要ります！！CorrectionChangeHandlerを参考に！！
        /// </summary>

        private ISpellSlot<Instance> m_model;
        
        private ISpellSlotView m_view;
        
        private CompositeDisposable m_valueChangeDisposable = new CompositeDisposable();

        public SpellSlotPresenter(ISpellSlot<Instance> model, ISpellSlotView view) {
            m_model = model ?? throw new ArgumentNullException();
            m_view = view ?? throw new ArgumentNullException();
        }

        public void Start() {
            RegisterChangeSpell();
            /*
            m_view.SpriteViewChange(m_model?.Spell.CurrentValue.Sprite);
            m_view.ValueViewChange(m_model.Spell.CurrentValue.Amount.Amount.CurrentValue);
            */
        }
        
        private void RegisterChangeSpell() {
            m_model
                .Spell
                    //スペルが変化したのを拾う
                .Subscribe(x => {
                    RegisterChangeValue();
                    //スプライトがないならスプライトを消す
                    if (m_model.Spell.CurrentValue.Sprite is null) {
                        m_view.RemoveSpriteView();
                    }
                    //あるならスプライトをセット
                    else {
                        m_view.SpriteViewChange(m_model.Spell.CurrentValue.Sprite);
                    }
                });
        }

        private void RegisterChangeValue() {
            
            m_valueChangeDisposable.Dispose();
            
            m_valueChangeDisposable = new CompositeDisposable();
            
            m_model.Spell.CurrentValue.Amount.Amount
                .Subscribe(x => {
                    if (x < 0) {
                        return;
                    }
                    m_view.ValueViewChange(x);
                })
                .AddTo(m_valueChangeDisposable);
        }
    }
}