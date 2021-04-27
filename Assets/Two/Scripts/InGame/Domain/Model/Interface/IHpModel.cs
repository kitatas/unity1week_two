using UniRx;

namespace Two.InGame.Domain.Model.Interface
{
    public interface IHpModel
    {
        IReadOnlyReactiveProperty<int> hp { get; }
        void SetHp(int value);
    }
}