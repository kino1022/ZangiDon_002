using System;
using JetBrains.Annotations;
using RinaBullet.Shooter.Interface;
using RinaBullet.Shooter.Pattern;
using RinaBullet.Symbol;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.CastAction.Interface;
using UnityEngine;
using VContainer;

namespace Src.Spell.CastAction {
    [Serializable]
    [Title("弾丸射出")]
    public class ShootBullet : ICastAction {

        [OdinSerialize]
        [LabelText("弾")]
        private Bullet m_prefab;
        
        [OdinSerialize]
        [LabelText("射撃パターン")]
        private IShootPattern m_pattern;
    
        public void Action([NotNull] GameObject caster, [NotNull] IObjectResolver resolver) {
            if (caster == null) throw new ArgumentNullException(nameof(caster));
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            var shooter = resolver.Resolve<IBulletShooter>() ?? throw new NullReferenceException();

            shooter.Shoot(m_prefab, m_pattern);
        }
    }
}