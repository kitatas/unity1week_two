namespace Two.Common.Domain.Repository.Interface
{
    public interface IPlayerDataRepository
    {
        string LoadName();
        void SaveName(string name);
        int LoadRate();
        void SaveRate(int rate);
    }
}