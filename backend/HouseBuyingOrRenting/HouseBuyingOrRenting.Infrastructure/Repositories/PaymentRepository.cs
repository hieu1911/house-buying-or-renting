using HouseBuyingOrRenting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Infrastructure
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly MyDbContext _db;

        public PaymentRepository(MyDbContext db) : base(db, db.Payments)
        {
            _db = db;
        }

        public async Task<Payment?> GetByRealEstateId(Guid id)
        {
            var result = await _db.Payments.SingleOrDefaultAsync(p => p.RealEstateId == id);
            return result;
        }
    }
}
