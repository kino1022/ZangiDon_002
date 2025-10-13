using RinaBullet.Shooter.Interface;
using RinaBullet.Shooter.Pattern;
using RinaBullet.Symbol;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using System;
using Sirenix.Serialization;

public class MockShootExecutor : SerializedMonoBehaviour {

    [OdinSerialize]
    private IShootPattern m_shootPattern;
    [OdinSerialize]
    private Bullet m_bulletPrefab;

    [OdinSerialize]
    private IBulletShooter m_shooter;

    private IObjectResolver m_resolver;

    [Inject]
    public void Construct(IObjectResolver resolver) {
        m_resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
    }

    private void Start() {
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            m_shooter.Shoot(m_bulletPrefab, m_shootPattern);
        }
    }
}
