using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        private readonly MyDbContext _db;

        public MessageRepository(MyDbContext db) : base(db, db.Messages)
        {
            _db = db;
        }

        public async Task<List<Message>> GetByUser(Guid senderId, Guid receiverId)
        {
            var result = _db.Messages
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.CreatedDate)
                .ToList();
            return result;
        }

        public async Task<List<Guid>> GetContactIds(Guid id)
        {
            var sender = _db.Messages.Where(m => m.SenderId == id).Select(m => m.ReceiverId).ToList();
            var receiver = _db.Messages.Where(m => m.ReceiverId == id).Select(m => m.SenderId).ToList();

            var result = new List<Guid>();
            result.AddRange(sender);
            result.AddRange(receiver);

            return result.Distinct().ToList();
        }
    }
}
