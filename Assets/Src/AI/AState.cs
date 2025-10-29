using System;
using VContainer.Unity;

namespace Src.AI {

    public interface IState {

        void Enter();

        void Update();
        
        void Exit();
        
    }
    
    public class AState {
        
    }
}