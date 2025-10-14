using Sirenix.OdinInspector;
using UnityEngine;

public class MockExplosion : SerializedMonoBehaviour
{
    private GameObject m_prefab;

    public void OnCollisionEnter(Collision collision) {
        GameObject.Instantiate(
            m_prefab,
            transform.position,
            transform.rotation
            );
        GameObject.Destroy( gameObject );
    }
}
