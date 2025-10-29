using System;
using System.Collections.Generic;
using System.Linq;
using GeneralModule.Symbol;
using JetBrains.Annotations;
using MessagePipe;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Health.EventBus;
using UnityEngine;
using VContainer;

namespace Src.GameManager.Entities {
    public class EntitiesManager : SerializedMonoBehaviour, IEntitiesProvider, IEntitiesManager{
        
        [OdinSerialize]
        [LabelText("存在しているエンティティ")]
        [ReadOnly]
        private List<ASerializedSymbol> m_entities = new List<ASerializedSymbol>();
        
        public IReadOnlyList<ASerializedSymbol> Entities => m_entities;
        
        private ISubscriber<IOnDeadEventBus> m_subscriber;
        
        private IDisposable m_subscription;
        
        private IObjectResolver m_resolver;
        
        public void Add (ASerializedSymbol entity) => m_entities.Add(entity);
        
        public void Remove (ASerializedSymbol entity) => m_entities.Remove(entity);
        
        public void Clear() => m_entities.Clear();

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_subscriber = m_resolver.Resolve<ISubscriber<IOnDeadEventBus>>();

            m_subscription = m_subscriber.Subscribe(OnTakeEventBus);
        }

        private void OnTakeEventBus(IOnDeadEventBus eventBus) {

            var entities = m_entities
                .Where(x => x.gameObject.transform.root.IsChildOf(eventBus.Object.transform))
                .ToList();

            if (entities.Count is 0) {
                Debug.Log($"{eventBus.Object.gameObject.name}が死亡した旨受け取りましたが管轄外でございました");
                return;
            }

            entities.ForEach(Remove);
        }
        
    }
}