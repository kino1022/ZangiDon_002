using System;
using GeneralModule.Lottery.Table;
using Src.Spell.Data;
using Src.Spell.Data.Interface;
using Src.Spell.Supplier.Pattern.Interface;

namespace Src.Spell.Supplier.Pattern {
    [Serializable]
    public class SpellSupplyPattern : LotteryTableCore<ASpellData> , ISpellSupplyPattern {

    }

}