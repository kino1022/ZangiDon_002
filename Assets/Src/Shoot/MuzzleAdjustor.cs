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
		[LabelText("銃口補正のスムーズさ")]
		private float m_smooth = 5.0f;
		
		private IObjectResolver m_resolver;

		[Inject]
		public void Construct(IObjectResolver resolver) {
			m_resolver = resolver;
		}

		private void Start() {
			m_targetProvider = m_resolver.Resolve<ITargetProvider>() ?? throw new ArgumentNullException();
		}

		private void Update() {
			Adjust();
		}

		public void Adjust() {

			var target = m_targetProvider.Target.CurrentValue;

			if (target is null) {
				return;
			}
			
			var direction = target.transform.position - transform.position;
			
			if (direction == Vector3.zero) return;
			
			Quaternion rotation = Quaternion.LookRotation(direction);
			
			transform.rotation = Quaternion.RotateTowards(
				transform.rotation,
				rotation,
				m_smooth * Time.deltaTime
			);
		}
    }
}