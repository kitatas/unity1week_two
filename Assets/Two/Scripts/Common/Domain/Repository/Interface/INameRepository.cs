namespace Two.Common.Domain.Repository.Interface
{
    public interface INameRepository
    {
        string Load();
        void Save(string name);
    }
}