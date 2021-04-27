using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.Model.Interface;
using Two.InGame.Domain.UseCase.Interface;

namespace Two.InGame.Domain.UseCase
{
    public sealed class HpUseCase : IHpUseCase
    {
        private readonly IHpEntity _hpEntity;
        private readonly IHpModel _hpModel;

        public HpUseCase(IHpEntity hpEntity, IHpModel hpModel)
        {
            _hpEntity = hpEntity;
            _hpModel = hpModel;
        }

        public void Damage()
        {
            _hpEntity.Decrease();
            _hpModel.SetHp(_hpEntity.GetHp());
        }

        public bool IsDead() => _hpEntity.GetHp() <= 0;
    }
}