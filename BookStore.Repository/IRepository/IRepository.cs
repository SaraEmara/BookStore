using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository.IRepository
{
    public interface IRepository<TEntity>
    {
       
            List<TEntity> GetALL();
            TEntity GetById(string id);
            void Insert(TEntity TEntity);
            void Update(TEntity entity);
            void Delete(TEntity TEntity);
        void AddToList(string Id,object Obj);

        
    }
}
