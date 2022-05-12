using Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace basketbackend.Data.Repository
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        public BasketRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Basket> GetWithItemsIncluded(int id)
        {
            return await _context.Baskets
                .Include(basket => basket.Items)
                .Where(basket => basket.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
