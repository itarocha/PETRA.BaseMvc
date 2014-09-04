using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petra.DAO
{
    public interface IDAO<T> : IDisposable 
        where T : class
    {
        T GetById(int id);
        void Delete(T entity);
        void Add(T entity);
        void Update(T entity);
        void SaveOrUpdate(T entity, int Id);
        IEnumerable<T> All();
    }
}
