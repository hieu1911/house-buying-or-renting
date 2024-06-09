using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public interface IPostSaveRepository : IBaseRepository<PostSave>
    {
        Task<PostSave> GetBuyUserIdAndRealEstateId(Guid userId, Guid realEstateId);

        Task<List<Guid>> GetRealEstateIdsByUser(Guid userId);
    }
}
