using HouseBuyingOrRenting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseBuyingOrRenting.Controllers
{
    public class BaseController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
    }
}
