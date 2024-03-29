using Instagram_Clone.Repositories;
using Instagram_Clone;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using TestingMVC.Repo;

namespace Instagram_Clone.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        Context context;
        public Repository(Context _context)
        {
            context = _context;
        }
        public void Delete(string id)
        {
            T t = GetById(id);
            Update(t);
        }

        public List<T> GetAll()//(string include=null)
        {
            
            return context.Set<T>().ToList();
           
            //return context.Set<T>().Include(include).ToList();
            //return context.Courses.Where(c => c.isDeleted == false).Include(c => c.Department).ToList();


        }

        public T GetById(string id)
        {
            return context.Set<T>().Find(id);
            // return context.Courses.Include(c => c.Department).FirstOrDefault(c => c.Id == id);
        }

        public void Insert(T obj)
        {
            context.Add(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            context.Update(obj);
        }
    }
}
