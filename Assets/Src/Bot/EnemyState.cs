using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Src.Bot {

    public interface IEnemyState {
        
        void Initialize(GameObject obj, IObjectResolver resolver);

        void Start();
        
        void Update();

        void Exit();
        
    }
    
}