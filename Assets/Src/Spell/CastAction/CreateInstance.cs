using Sirenix.OdinInspector;
using Src.Spawner;
using Src.Spell.CastAction.Interface;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Src.Spell.CastAction {
    [Serializable]
    public class CreateInstance : IPreCastAction, IPostCastAction {

        [LabelText("生成するインスタンス")]
        private GameObject m_prefab;

        [LabelText("生成位置")]
        private ISpawnPositionProvider m_posProvider;

        public void Action (GameObject obj, IObjectResolver resolver) {

            if (obj is null) {
                return;
            }

            if (m_posProvider is null) {
                return;
            }

            var pos = m_posProvider.Provide(obj, resolver);

            resolver.Instantiate(
                m_prefab,
                pos,
                Quaternion.identity
                );
        }
    }
}