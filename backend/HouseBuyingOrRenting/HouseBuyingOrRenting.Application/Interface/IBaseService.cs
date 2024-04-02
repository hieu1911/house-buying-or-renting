using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface IBaseService<TEntity, TEntityCreateDto, TEntityUpdateDto> 
        where TEntity : BaseEntity where TEntityCreateDto : class where TEntityUpdateDto : class
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(Guid id);

        Task<int> InsertAsync(TEntityCreateDto entityCreateDto);

        Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto);

        Task<int> DeleteAsync(Guid id);
    }
}
