using R3;
using Src.Spell.Instance.Interface;

namespace Src.Spell.Slot.Interface {
    public interface ISpellSlot<Instance> where Instance : ISpellInstance {
        
        /// <summary>
        /// セットされているスペル
        /// </summary>
        ReadOnlyReactiveProperty<Instance> Spell { get; }
        
        /// <summary>
        /// スロットが空で新しいインスタンスをセットできるかどうか
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// スペルをセットする
        /// </summary>
        /// <param name="spell"></param>
        void Set(Instance spell);

        /// <summary>
        /// スペルを除外する
        /// </summary>
        void Remove();
        
    }
}