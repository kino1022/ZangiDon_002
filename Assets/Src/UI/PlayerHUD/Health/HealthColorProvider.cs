using System.Drawing;

namespace Src.UI.PlayerHUD.Health {
    /// <summary>
    /// 体力の割合に応じた表示色を管理するクラス
    /// </summary>
    public interface IPercentColor {

        /// <summary>
        /// この色での表示が始まる割合
        /// </summary>
        float StartPersent { get; }

        /// <summary>
        /// この色での表示が終わる割合
        /// </summary>
        float EndPersent { get; }

        /// <summary>
        /// 該当する割合の際の色
        /// </summary>
        Color Color { get; }

    }
}