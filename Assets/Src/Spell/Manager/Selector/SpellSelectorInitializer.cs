using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Src.Spell.Manager.Selector.Interface;
using UnityEngine;

namespace Src.Spell.Manager.Selector {
    [Serializable]
    public class SpellSelectorInitializer : ISpellSelectorInitializer {

        [SerializeField]
        [LabelText("初期化値")]
        private int m_initAmount = 6;

        public void Initialize([NotNull]ISpellSelector selector) {
            
            Debug.Assert(CheckIntegrate(selector),$"{selector.GetType().Name}の初期化に指定された値が不正です");
            
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            for (int i = 0; i < m_initAmount; i++) {
                selector.Supply();
            }
        }

        /// <summary>
        /// 初期化数の整合性チェック
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private bool CheckIntegrate(ISpellSelector selector) {

            if (selector.Length < m_initAmount || m_initAmount < 0) {
                return false;
            }
            
            return true;
        }
    }
}