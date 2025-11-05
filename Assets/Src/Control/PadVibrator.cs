using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using Sirenix.OdinInspector;
using Src.Health.EventBus;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using R3;
using Src.Health;

namespace Src.Control {
    public class PadVibrator : SerializedMonoBehaviour {
        
        [Title("設定")]
        
        [SerializeField]
        [LabelText("振動時間")]
        private float m_duration = 0.5f;
        
        [SerializeField]
        [LabelText("低周波")]
        private float m_lowFrequency = 0.5f;
        
        [SerializeField]
        [LabelText("高周波")]
        private float m_highFrequency = 0.5f;
        
        [Title("参照")]
        
        [SerializeField]
        [LabelText("プレイヤー")]
        [ReadOnly]
        private Player.Player m_player;
        
        [SerializeField]
        [LabelText("体力")]
        [ReadOnly]
        private Health.IHealth m_health;
        
        [SerializeField]
        [LabelText("最大体力")]
        [ReadOnly]
        private IMaxHealth m_maxHealth;

        [SerializeField]
        [LabelText("振動させるコントローラー")]
        [ReadOnly]
        private Gamepad m_pad;

        private ISubscriber<ITakeDamageEventBus> m_subscriber;

        private IDisposable m_subscription;

        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver;
        }

        private void Start() {
            m_player = m_resolver.Resolve<Player.Player>();

            m_subscriber = m_resolver.Resolve<ISubscriber<ITakeDamageEventBus>>();

            m_subscription = m_subscriber.Subscribe(OnTakeEventBus);
            
            m_subscription.AddTo(this);
            
            m_pad = Gamepad.current;
        }

        private void OnTakeEventBus(ITakeDamageEventBus eventBus) {
            
            if (eventBus.Object.transform.root.gameObject != m_player.transform.root.gameObject) {
                Debug.Log("プレイヤーに対してのダメージではなかったので振動処理をキャンセルします");
                return;
            }
            
            Debug.Log("プレイヤーがダメージを受けたのでコントローラーを振動させます");

            if (m_pad == null) {
                m_pad = Gamepad.current;
                if (m_pad == null) {
                    Debug.Log("コントローラーが接続されていないため振動処理をキャンセルします");
                    return;
                }
            }
            
            Debug.Log("コントローラーの振動を開始します");
            
            Vibration(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask Vibration(CancellationToken token) {
            try {
                m_pad.SetMotorSpeeds(m_lowFrequency, m_highFrequency);
                
                await UniTask.Delay(TimeSpan.FromSeconds(m_duration), cancellationToken: token);
            }
            catch (OperationCanceledException) {

            }
            finally {
                m_pad.SetMotorSpeeds(0f, 0f);
            }
        }
    }
}