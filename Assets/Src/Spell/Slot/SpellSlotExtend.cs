using System;
using R3;
using Src.Spell.Instance.Interface;
using Src.Spell.Slot.Interface;

namespace Src.Spell.Slot {
    public static class SpellSlotExtend {
        
        /// <summary>
        /// 指定したスロットのスペルの使用回数が0になった場合に流れるストリームを提供する
        /// </summary>
        /// <param name="slot"></param>
        /// <typeparam name="Instance"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Observable<Unit> RegisterAmountZero<Instance>(this ISpellSlot<Instance> slot)
            where Instance : ISpellInstance 
        {
            if (slot is null || slot.Spell.CurrentValue is null) {
                throw new ArgumentNullException();
            }
            
            return slot
                .Spell
                .CurrentValue
                .Amount
                .Amount
                .Where(x => x == 0)
                .Select(_ => Unit.Default);
        }
        
    }
}