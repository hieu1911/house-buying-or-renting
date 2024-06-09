using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface IPostSaveService : IBaseService<PostSave, PostSave, PostSaveCreateDto, PostSave>
    {
        Task<PostSave> GetBuyUserIdAndRealEstateId(Guid userId, Guid realEstateId);

        Task<List<Guid>> GetRealEstateIdsByUser(Guid userId);
    }
}
