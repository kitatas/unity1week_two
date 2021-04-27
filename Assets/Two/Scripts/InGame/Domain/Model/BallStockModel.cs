using Two.InGame.Domain.Model.Interface;
using UniRx;

namespace Two.InGame.Domain.Model
{
    public sealed class BallStockModel : IBallStockModel
    {
        private readonly ReactiveProperty<int> _stockCount;

        public BallStockModel()
        {
            _stockCount = new ReactiveProperty<int>(0);
        }

        public IReadOnlyReactiveProperty<int> stockCount => _stockCount;

        public void SetStockCount(int value)
        {
            _stockCount.Value = value;
        }
    }
}