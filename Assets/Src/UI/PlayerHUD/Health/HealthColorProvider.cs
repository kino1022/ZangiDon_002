using System.Drawing;

namespace Src.UI.PlayerHUD.Health {
    /// <summary>
    /// �̗͂̊����ɉ������\���F���Ǘ�����N���X
    /// </summary>
    public interface IPercentColor {

        /// <summary>
        /// ���̐F�ł̕\�����n�܂銄��
        /// </summary>
        float StartPersent { get; }

        /// <summary>
        /// ���̐F�ł̕\�����I��銄��
        /// </summary>
        float EndPersent { get; }

        /// <summary>
        /// �Y�����銄���̍ۂ̐F
        /// </summary>
        Color Color { get; }

    }
}