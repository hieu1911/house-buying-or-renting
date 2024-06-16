using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public interface IMessageService : IBaseService<Message, Message, MessageCreateDto, Message>
    {
        Task<List<Message>> GetByUser(Guid senderId, Guid receiverId);

        Task<List<Guid>> GetContactIds(Guid id);
    }
}
