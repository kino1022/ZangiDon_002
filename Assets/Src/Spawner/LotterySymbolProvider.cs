using System;
using GeneralModule.Lottery.Table;
using GeneralModule.Symbol;

namespace Src.Spawner {
    
    [Serializable]
    public class LotterySymbolProvider : LotteryTableCore<ASerializedSymbol>, ISpawnSymbolProvider {
        public ASerializedSymbol Provide() => Lottery();
        
    }
}