namespace Src.GameOver {

    public interface IGameOverEventBus {
        
        /// <summary>
        /// 到達したウェーブ
        /// </summary>
        int FinalWave { get; }
        
    }
    
    public readonly struct GameOverEventBus : IGameOverEventBus {
        
        public int FinalWave { get; init; }

        public GameOverEventBus(int wave) {
            FinalWave = wave;
        }
    }
}