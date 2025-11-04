using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Target;
using UnityEngine;
using VContainer;

namespace Src.Shoot {

	/// <summary>
	/// 銃口の補正を行うクラスに対して約束するインターフェイス
	/// </summary>
	public interface IMuzzleAdjustor {
		
		/// <summary>
		/// 銃口の調整を行うクラス
		/// </summary>
		void Adjust();
		
	}

	public class MuzzleAdjustor : SerializedMonoBehaviour, IMuzzleAdjustor {
		
		[OdinSerialize]
		[LabelText("ターゲット管理クラス")]
		[ReadOnly]
		private ITargetProvider m_targetProvider;

	

		[SerializeField] 
		private float m_heightOffset = 1.5f;
		
		private IObjectResolver m_resolver;

		[Inject]
		public void Construct(IObjectResolver resolver) {
			m_resolver = resolver;
		}

		private void Start() {
			// DI が正しく行われているか確認して取得
			if (m_resolver == null) throw new InvalidOperationException("IObjectResolver was not injected into MuzzleAdjustor");
			m_targetProvider = m_resolver.Resolve<ITargetProvider>() ?? throw new InvalidOperationException("ITargetProvider could not be resolved in MuzzleAdjustor");
		}

		private void Update() {
			// 安全のため null チェック
			if (m_targetProvider == null) return;
			Adjust();
		}

		public void Adjust() {

			var target = m_targetProvider.Target.CurrentValue;

			if (target is null) {
				return;
			}
			
			// 注視点はターゲットの位置に高さオフセットを加えたワールド座標
			var lookAtPosition = target.transform.position + Vector3.up * m_heightOffset;
			
			var direction = lookAtPosition - transform.position;
			
			// 浮動小数点の等価判定は避ける
			if (direction.sqrMagnitude < 1e-6f) return;
			
			transform.LookAt(lookAtPosition);
		}
    }
}