using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Domain
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<List<Message>> GetByUser(Guid senderId, Guid receiverId);

        Task<List<Guid>> GetContactIds(Guid id);
    }
}
