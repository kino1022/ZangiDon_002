using R3;
using RinaInput.Controller.Module;
using RinaInput.Lever.Direction.Definition;
using RinaInput.Signal;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.Manager.Selector.Interface;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Src.Control {

    public interface ISpellSelectAction {
        
        public int SelectIndex { get; }
        
    }
    
    public class SpellSelect : SerializedMonoBehaviour, ISpellSelectAction {
        
        [Title("入力モジュール")]

        [OdinSerialize]
        [LabelText("選択用スティック")]
        private IInputModule<Vector2> m_stick;

        [OdinSerialize]
        [LabelText("決定用トリガー")]
        private IInputModule<float> m_trigger;

        [SerializeField]
        [LabelText("選択中インデックス")]
        [ReadOnly]
        private int m_selectIndex = 0;
        
        [Title("参照")]

        [OdinSerialize]
        [LabelText("セレクター")]
        [ReadOnly]
        private ISpellSelector m_selector;
        
        private IObjectResolver m_resolver;
        
        public int SelectIndex => m_selectIndex;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        public void Start() {
            
            m_selector = m_resolver.Resolve<ISpellSelector>();
            
            RegisterStream();
            
        }

        private void RegisterStream() {
            
            m_stick
                .Stream
                .Where(x => x.Phase != InputActionPhase.Canceled)
                .Subscribe(x => {
                    m_selectIndex = GetDirectionIndexFromUp(x.Value, m_selector.Spells.Count);
                })
                .AddTo(this);
            
            m_trigger
                .Stream
                .Where(x => x.Phase != InputActionPhase.Canceled)
                .Subscribe(x => {
                    m_selector.Select(m_selectIndex);
                })
                .AddTo(this);
                
        }

        private int CalculateIndex(InputSignal<Vector2> inputSignal) {
            return inputSignal
                .Value
                .GetDirectionIndex(m_selector.Spells.Count, 0.0f);
        } 
        
        public static int GetDirectionIndexFromUp(Vector2 input, int directions, float deadZone = 0.2f)
        {
            // 1. デッドゾーンのチェック
            if (input.magnitude < deadZone)
            {
                return -1; // 方向なし
            }

            // 2. 角度の計算 (ラジアンから度に変換)
            // Mathf.Atan2(-x, y) は、Y軸正方向(上)を0度とし、
            // 反時計回りに -180 ～ 180 の範囲で返します。
            // (例: 左が 90度、右が -90度)
            float angleRad = Mathf.Atan2(input.x, input.y);
            float angleDeg = angleRad * Mathf.Rad2Deg;

            // 3. 角度を 0 ～ 360 の範囲に正規化
            // (例: -90度(右) を 270度 に変換)
            if (angleDeg < 0)
            {
                angleDeg += 360f;
            }

            // 4. 各方向が担当する角度の「スライス幅」を計算
            // (例: 8方向なら 360 / 8 = 45度)
            float slice = 360f / directions;

            // 5. 判定の境界をずらすためのオフセット（スライス幅の半分）
            // (例: 8方向なら 45 / 2 = 22.5度 を足す)
            float offsetAngle = angleDeg + (slice / 2f);

            // 6. 角度からインデックスを計算
            int index = Mathf.FloorToInt(offsetAngle / slice);

            // 7. インデックスを 0 ～ (directions-1) の範囲に丸める
            return index % directions;
        }
    }
}