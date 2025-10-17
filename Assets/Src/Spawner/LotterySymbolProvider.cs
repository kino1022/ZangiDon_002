using System;
using GeneralModule.Lottery.Table;
using GeneralModule.Symbol;
using UnityEngine;
using VContainer;

namespace Src.Spawner {
    
    [Serializable]
    public class LotterySymbolProvider : LotteryTableCore<ASerializedSymbol>, ISpawnSymbolProvider {
        public ASerializedSymbol Provide(GameObject spawner, IObjectResolver resolver) => Lottery();
        
    }
}