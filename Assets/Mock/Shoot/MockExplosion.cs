using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class MockExplosion : SerializedMonoBehaviour
{
    [SerializeField]
    [LabelText("爆発エフェクト")]
    private GameObject m_prefab;

    public void OnCollisionEnter(Collision collision) {
        var instance = GameObject.Instantiate(
            m_prefab,
            transform.position,
            transform.rotation
            );
        InvokeDestroy(instance).Forget();
        GameObject.Destroy( gameObject );
    }

    private async UniTask InvokeDestroy(GameObject argobj) {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        Destroy(argobj);
    }
}
