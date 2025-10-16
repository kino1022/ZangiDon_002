using System.Linq;
using GeneralModule.Symbol;
using R3;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.GameManager.Entities;
using UnityEngine;
using VContainer;

namespace Src.Target {
    public class TargetManager : SerializedMonoBehaviour, ITargetProvider, ITargetManager {

        private ReactiveProperty<GameObject> m_target;
        
        
        [Title("参照")]
        
        [OdinSerialize]
        [ReadOnly]
        [LabelText("エンティティリスト")]
        private IEntitiesProvider m_entitiesProvider;
        
        private IObjectResolver m_resolver;
        
        public ReadOnlyReactiveProperty<GameObject> Target => m_target;
        
        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }
        
        private void Awake() {
            
            m_target = new ReactiveProperty<GameObject>();
            
        }

        private void Start() {
            
            m_entitiesProvider = m_resolver.Resolve<IEntitiesProvider>();
            
        }

        private void Update() {
            //ターゲットが消滅した場合の処理
            if (m_target.Value == null) {
                m_target.Value = GetNearTarget().gameObject;
            }
        }

        public void ChangeTarget(GameObject target) {
            throw new System.NotImplementedException();
        }

        public void DisTarget() {
            throw new System.NotImplementedException();
        }

        private ASerializedSymbol GetNearTarget() {
            
            var list = m_entitiesProvider.Entities;

            //Entityが存在しなかった場合の処理
            if (list.Count == 0) {
                return null;
            }

            var result = list.First();
            
            var distance = Vector3.Distance(gameObject.transform.position, result.transform.position);

            foreach (var entity in list) {
                var tempDistance = Vector3.Distance(gameObject.transform.position, entity.transform.position);
                if (tempDistance > distance) {
                    result = entity;
                    distance = tempDistance;
                }
            }
            
            return result;
        }
    }
}