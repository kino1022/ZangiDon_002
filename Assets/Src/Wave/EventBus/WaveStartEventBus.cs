namespace Src.Wave.EventBus {

    public interface IWaveStartEventBus {
        
        int WaveCount { get; }
        
    }
    
    public readonly struct WaveStartEventBus : IWaveStartEventBus {
        
        public int WaveCount { get; init; }


        public WaveStartEventBus(int count) {

            if (count <= 0) {
                throw new System.ArgumentOutOfRangeException(nameof(count));
            }
            
            WaveCount = count;
        }
    }
}