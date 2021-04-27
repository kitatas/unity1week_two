using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;

namespace Two.InGame.Data.Entity
{
    public sealed class HpEntity : IHpEntity
    {
        private int _hp;

        public HpEntity()
        {
            SetHp(PlayerParam.MAX_HP);
        }

        public int GetHp() => _hp;

        public void SetHp(int value) => _hp = value;

        public void Decrease() => _hp--;
    }
}