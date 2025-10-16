using System;
using System.Linq;
using Src.Spell.Instance.Interface;
using Src.Spell.Manager.Interface;
using Src.Spell.Slot.Interface;

namespace Src.Spell.Manager {
    public static class SpellManagerExtension {

        /// <summary>
        /// スペルマネージャーが満タンかどうかを返すメソッド
        /// </summary>
        /// <param name="manager"></param>
        /// <typeparam name="Instance"></typeparam>
        /// <typeparam name="Slot"></typeparam>
        /// <returns></returns>
        public static bool IsFull<Instance, Slot>(this ISpellManager<Instance, Slot> manager)
            where Instance : ISpellInstance
            where Slot : ISpellSlot<Instance> 
        {
            foreach (var pair in manager.Spells) {
                if (pair.Value.IsEmpty) return false;
            }
            return true;
        }

        /// <summary>
        /// スペルが満タンではないかどうかを返すメソッド
        /// </summary>
        /// <param name="manager"></param>
        /// <typeparam name="Instance"></typeparam>
        /// <typeparam name="Slot"></typeparam>
        /// <returns></returns>
        public static bool IsEmpty<Instance, Slot>(this ISpellManager<Instance, Slot> manager)
            where Instance : ISpellInstance
            where Slot : ISpellSlot<Instance> {
            return !manager.IsFull();
        }

        /// <summary>
        /// 管理しているスペルの使用回数を一括で減少させる
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="amount"></param>
        /// <typeparam name="Instance"></typeparam>
        /// <typeparam name="Slot"></typeparam>
        public static void DecreaseAmount<Instance, Slot>(this ISpellManager<Instance, Slot> manager, int amount)
            where Instance : ISpellInstance
            where Slot : ISpellSlot<Instance> 
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException();
            
            foreach (var pair in manager.Spells) {
                var slot = pair.Value ?? throw new NullReferenceException();
                //スロットが空なら戻る
                if (slot.IsEmpty) continue;

                var spell = slot.Spell.CurrentValue;
                //スペルがないなら戻る(本来あり得ないから処理は要検討)
                if (spell is null) continue;
                
                spell.Amount.Decrease(amount);
            }
        }

        /// <summary>
        /// 空のスロットの中で一番若いものを返すメソッド
        /// </summary>
        /// <param name="manager"></param>
        /// <typeparam name="Instance"></typeparam>
        /// <typeparam name="Slot"></typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">空のスロットが無い場合に返す</exception>
        public static Slot GetFirstEmptySlot<Instance, Slot>(this ISpellManager<Instance, Slot> manager)
            where Instance : ISpellInstance
            where Slot : ISpellSlot<Instance> 
        {
            var slots = manager.Spells.Select(x => x.Value ?? throw new NullReferenceException()).ToList();

            foreach (var slot in slots) {
                if (slot.IsEmpty) return slot;
            }
            
            throw new NullReferenceException();
        }
    }
}