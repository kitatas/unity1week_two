namespace Two.InGame.Data.Entity.Interface
{
    public interface IHpEntity
    {
        int GetHp();
        void SetHp(int value);
        void Decrease();
    }
}