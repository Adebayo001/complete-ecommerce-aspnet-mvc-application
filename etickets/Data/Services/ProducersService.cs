using etickets.Data.Base;
using etickets.Models;

namespace etickets.Data.Services
{
    public class ProducersService:EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context) { }

    }
}
