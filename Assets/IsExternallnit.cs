using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// C# 9.0 の init アクセサーを古いフレームワークで利用するための互換性クラス。
    /// このクラスをプロジェクトに追加することでコンパイルエラーが解消されます。
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit
    {
    }
}