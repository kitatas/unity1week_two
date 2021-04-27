using UniRx;

namespace Two.InGame.Domain.Model.Interface
{
    public interface IBallStockModel
    {
        IReadOnlyReactiveProperty<int> stockCount { get; }
        void SetStockCount(int value);
    }
}