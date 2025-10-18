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
    public class SpellSlotPresenter<Instance> : ISpellSlotPresenter where Instance : ISpellInstance {

        private ISpellSlot<Instance> m_model;
        
        private ISpellSlotView m_view;
        
        /// <summary>
        /// スペルスロット自体のDisposable
        /// </summary>
        private CompositeDisposable m_rootDisposable = new CompositeDisposable();
        
        /// <summary>
        /// スロットの中のスペルに対するDisposable
        /// </summary>
        private CompositeDisposable m_spellDispoable = new CompositeDisposable();
        
        public SpellSlotPresenter(ISpellSlot<Instance> model, ISpellSlotView view) {
            m_model = model ?? throw new ArgumentNullException();
            m_view = view ?? throw new ArgumentNullException();
        }

        public void Start() {

            m_rootDisposable = new();
            
            //スペルスロットの監視処理
            RegisterSpellSlot();
        }

        private void RegisterSpellSlot() {
            m_model
                .Spell
                .Subscribe(OnSpellChanged)
                .AddTo(m_rootDisposable);
        }

        private void OnSpellChanged(Instance spell) {
            
            //変化後のスペルが空だった場合の処理
            if (spell is null) {
                m_view.RemoveSpriteView();
                m_view.ValueViewChange(0);
                m_spellDispoable.Dispose();
                return;
            }
            
            RegisterSpellChange(spell);
            m_view.SpriteViewChange(spell.Sprite);
        }

        private void RegisterSpellChange(Instance spell) {

            m_spellDispoable = new();
            
            spell
                .Amount
                .Amount
                .Subscribe(m_view.ValueViewChange)
                .AddTo(m_spellDispoable);
        }
    }
}