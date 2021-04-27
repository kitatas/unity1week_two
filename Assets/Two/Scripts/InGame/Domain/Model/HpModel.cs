using Two.InGame.Application;
using Two.InGame.Domain.Model.Interface;
using UniRx;

namespace Two.InGame.Domain.Model
{
    public sealed class HpModel : IHpModel
    {
        private readonly ReactiveProperty<int> _hp;

        public HpModel()
        {
            _hp = new ReactiveProperty<int>(PlayerParam.MAX_HP);
        }

        public IReadOnlyReactiveProperty<int> hp => _hp;

        public void SetHp(int value)
        {
            _hp.Value = value;
        }
    }
}