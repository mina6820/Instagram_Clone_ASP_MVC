
namespace TestingMVC.Repo
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll(string? include=null);
        public T GetById(int id);
        public void Insert(T obj);
        public void Update(T obj);
        public void Delete(int id);
        public void Save();
    }
}