using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using RinaBullet.Context;
using RinaBullet.Context.Container;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Src.Spell.CastAction.Interface;
using UnityEngine;
using VContainer;

namespace Src.Spell.CastAction {
    [Serializable]
    public class AddBulletContext : IPreCastAction {

        [OdinSerialize]
        [LabelText("追加するコンテキスト")]
        private List<IBulletContext> m_contexts;
        
        public void Action([NotNull] GameObject caster, [NotNull] IObjectResolver resolver) {
            
            if (caster == null) throw new ArgumentNullException(nameof(caster));
            
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            if (m_contexts.Count is 0) {
                return;
            }

            var container = resolver.Resolve<IContextContainer>();
            
            m_contexts.ForEach(x => container.Add(x));
        }
    }
}