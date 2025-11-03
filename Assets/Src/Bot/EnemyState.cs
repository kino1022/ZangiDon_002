using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Src.Bot {

    public interface IEnemyState {

        void SetSelf(GameObject enemy);

        void Enter();
        
        void Update();

        void Exit();
        
    }
    
}