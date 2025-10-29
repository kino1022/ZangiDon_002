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
        
        
        #if UNITY_EDITOR
        [ShowInInspector]
        private GameObject m_serialzeTarget => m_target.Value;
        #endif
        
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
            
            RegisterEmptyTarget();
            
        }
        
        public void ChangeTarget(GameObject target) {
            m_target.Value = target;
        }

        public void DisTarget() {
            m_target.Value = null;
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

        private void RegisterEmptyTarget() {
            

            m_target
                .Do(_=> Debug.Log($"{GetType().Name}のターゲットが空になるまでの待機処理を開始します"))
                .Where(x => x is null)
                .Subscribe(_ => {
                    var next = GetNearTarget();
                    
                    if (next is null) {
                        RegisterAddEntity();
                        return;
                    }
                    
                    m_target.Value = next.gameObject;
                    
                })
                .AddTo(this);
        }

        private void RegisterAddEntity() {

            Observable
                .EveryValueChanged(m_entitiesProvider, x => x.Entities.Count)
                .Do(_=> Debug.Log($"{m_entitiesProvider.GetType().Name}の追加を待機します"))
                .Where(x => x is not 0)
                .Subscribe(_ => {
                    
                    m_target.Value = GetNearTarget().gameObject;
                    
                })
                .AddTo(this);
        }
    }
}