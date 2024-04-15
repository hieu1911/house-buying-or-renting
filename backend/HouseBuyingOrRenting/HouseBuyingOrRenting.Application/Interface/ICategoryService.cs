using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface IImageUrlService : IBaseService<ImageUrl, ImageUrlDto, ImageUrlCreateDto, ImageUrlUpdateDto>
    {
    }
}
