namespace Two.InGame.Domain.Repository.Interface
{
    public interface INameRepository
    {
        string Load();
        void Save(string name);
    }
}