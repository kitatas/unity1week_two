using Two.Common.Application;
using Two.Common.Presentation.Controller.Sound;
using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.Model.Interface;
using Two.InGame.Domain.UseCase.Interface;

namespace Two.InGame.Domain.UseCase
{
    public sealed class HpUseCase : IHpUseCase
    {
        private readonly IHpEntity _hpEntity;
        private readonly IHpModel _hpModel;
        private SeController _seController;

        public HpUseCase(IHpEntity hpEntity, IHpModel hpModel, SeController seController)
        {
            _hpEntity = hpEntity;
            _hpModel = hpModel;
            _seController = seController;
        }

        public void Damage()
        {
            _seController.Play(SeType.Damage);
            _hpEntity.Decrease();
            _hpModel.SetHp(_hpEntity.GetHp());
        }

        public bool IsDead() => _hpEntity.GetHp() <= 0;
    }
}