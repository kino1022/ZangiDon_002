using System;
using System.Collections.Generic;
using GeneralModule.Symbol;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Src.GameManager.Entities {
    public class EntitiesManager : SerializedMonoBehaviour, IEntitiesProvider, IEntitiesManager{
        
        [OdinSerialize]
        [LabelText("存在しているエンティティ")]
        [ReadOnly]
        private List<ASerializedSymbol> m_entities = new List<ASerializedSymbol>();
        
        public IReadOnlyList<ASerializedSymbol> Entities => m_entities;
        
        public void Add (ASerializedSymbol entity) => m_entities.Add(entity);
        
        public void Remove (ASerializedSymbol entity) => m_entities.Remove(entity);
        
        public void Clear() => m_entities.Clear();
    }
}