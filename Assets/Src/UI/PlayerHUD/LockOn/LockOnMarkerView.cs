using Sirenix.OdinInspector;
using Src.Utility;
using UnityEngine;

namespace Src.UI.PlayerHUD.LockOn {

    public interface ILockOnMarkerView {

        void ChangeScreenTransform(Vector2 pos);

        void SetVisibility (bool visible);

    }

    public class LockOnMarkerView : SerializedMonoBehaviour, ILockOnMarkerView {

        [SerializeField]
        private GameObject m_object;

        private RectTransform m_rectTransform;

        private void Start () {
            m_rectTransform = ComponentsUtility.GetComponentsFromWhole<RectTransform>(gameObject);
        }

        public void ChangeScreenTransform(Vector2 pos) {
            m_rectTransform.position = pos;
        }

        public void SetVisibility (bool visible) {
            m_object.SetActive(visible);
        }

    }
}