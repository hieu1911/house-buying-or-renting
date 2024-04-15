using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> 
        where TEntity : BaseEntity where TEntityDto : class where TEntityCreateDto : class where TEntityUpdateDto : class
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(Guid id);

        Task<int> InsertAsync(TEntityCreateDto entityCreateDto);

        Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto);

        Task<int> DeleteAsync(Guid id);
    }
}
