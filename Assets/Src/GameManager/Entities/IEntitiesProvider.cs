using System.Collections.Generic;
using GeneralModule.Symbol;

namespace Src.GameManager.Entities {
    /// <summary>
    /// 存在しているエンティティを管理するクラスに対して約束するインタフェース
    /// </summary>
    public interface IEntitiesProvider {
        
        IReadOnlyList<ASerializedSymbol> Entities { get; }
        
    }
}