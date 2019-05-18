using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository.Mapper
{
    public interface IMapper<TEntity,TDTO>
    {
        TDTO MapToDTO(TEntity Obj);
        TEntity MapToDbEntity(TDTO Obj);
    }
}
