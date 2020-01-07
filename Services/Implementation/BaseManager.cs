using Data;
using Services.Interface;

namespace Services.Interfaces
{

    public abstract class BaseManager<T> where T : ICustomModel
    {
        protected WebStoreDbContext context;
        public BaseManager(WebStoreDbContext db)
        {
            this.context = db;
        }
        public abstract string Add(T model);
        public abstract string Update(T model);
        public abstract string Delete(T model);
        public abstract T Get(int id);
    }
}
