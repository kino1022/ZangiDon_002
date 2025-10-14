using Sirenix.OdinInspector;
using UnityEngine;

namespace System.Runtime.CompilerServices.Mock.Shoot {
    public class MockDestroy : SerializedMonoBehaviour {
        public void OnCollisionEnter(Collision collision) {
            Destroy(gameObject);
        }
    }
}