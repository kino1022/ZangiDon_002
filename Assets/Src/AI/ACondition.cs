using R3;

namespace Src.AI {

    public interface ICondition {
        
        /// <summary>
        /// 条件が満たされているかどうか
        /// </summary>
        ReadOnlyReactiveProperty<bool> IsFill { get; }

    }
    
    public abstract class ACondition : ICondition {
        
        private ReactiveProperty<bool> m_condition = new ReactiveProperty<bool>();
        
        public ReadOnlyReactiveProperty<bool> IsFill => m_condition;
        
    }
}