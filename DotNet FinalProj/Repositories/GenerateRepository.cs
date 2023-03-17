using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj.Repositories
{
    public class GenerateRepository<T> : IDisposable where T : class
    {
        DbContext context;
        DbSet<T> set;
        public GenerateRepository(DbContext context)
        {
            this.context = context;
            set = this.context.Set<T>();
        }
        public void CreateOrUpdate(T entity) => set.AddOrUpdate(entity);
        public void Delete(T entity) => set.Remove(entity);
        public T Get(int id) => set.Find(id);
        public IEnumerable<T> GetAll() => set;
        public void Save() => context.SaveChanges();
        public void Dispose()
        {
            context.Dispose();
        }
        ~GenerateRepository()
        {
            context.Dispose();
        }
    }
}
