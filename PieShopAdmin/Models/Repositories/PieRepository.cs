using Microsoft.EntityFrameworkCore;
using PieShopAdmin.Database;

namespace PieShopAdmin.Models.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly PieShopDbContext _pieShopDbContext;

        public PieRepository(PieShopDbContext pieShopDbContext)
        {
            _pieShopDbContext = pieShopDbContext;
        }

        public async Task<int> AddPieAsync(Pie pie)
        {
            _pieShopDbContext.Pies.Add(pie);
            return await _pieShopDbContext.SaveChangesAsync();
        }

        public async Task<int> DeletePieAsync(int id)
        {
            var pieToDelete = await _pieShopDbContext.Pies.FirstOrDefaultAsync(x => x.PieId == id);

            if(pieToDelete != null)
            {
                _pieShopDbContext.Pies.Remove(pieToDelete);
                return await _pieShopDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("The pie to delete can't be found");
            }
        }

        //when we read lists we dont need to truck changes
        public async Task<IEnumerable<Pie>> GetAllPiesAsync()
        {
            return await _pieShopDbContext.Pies.OrderBy(x => x.PieId).AsNoTracking().ToListAsync();
        }

        public async Task<int> GetAllPiesCountAsync()
        {
            var count = await _pieShopDbContext.Pies.CountAsync();
            return count;
        }

        public async Task<Pie>? GetPieByIdAsync(int id)
        {
            return await _pieShopDbContext.Pies.Include(x => x.Ingredients).Include(p => p.Category).FirstOrDefaultAsync(z => z.PieId == id);
        }

        public async Task<IEnumerable<Pie>> GetPiesPagedAsync(int? pageNumber, int pageSize)
        {

            //IQuerybale because in final request to database we only get <PageSize> amount of pies not all
            IQueryable<Pie> pies = from p in _pieShopDbContext.Pies
                                   select p;

            pageNumber ??= 1;
            pies = pies.Skip((pageNumber.Value - 1) * pageSize).Take(Consts.PageSize);
            return await pies.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Pie>> GetPiesSortedAndPagedAsync(string sortBy, int? pageNumber, int pageSize)
        {
            //IQuerybale because in final request to database we only get <PageSize> amount of pies not all
            IQueryable<Pie> pies = from p in _pieShopDbContext.Pies
                                   select p;
        
            switch(sortBy)
            {
                case Consts.NameDesc:
                    pies = pies.OrderByDescending(p => p.Name);
                    break;
                case Consts.Name:
                    pies = pies.OrderBy(p => p.Name);
                    break;
                case Consts.IdDesc:
                    pies = pies.OrderByDescending(p => p.PieId);
                    break;
                case Consts.Id:
                    pies = pies.OrderBy(p => p.PieId);
                    break;
                case Consts.PriceDesc:
                    pies = pies.OrderByDescending(p => p.Price);
                    break;
                case Consts.Price:
                    pies = pies.OrderBy(p => p.Price);
                    break;
                default:
                    pies = pies.OrderBy(p => p.PieId);
                    break;
            }

            pageNumber ??= 1;
            pies = pies.Skip((pageNumber.Value -1) * pageSize).Take(pageSize);
            return await pies.AsNoTracking().ToListAsync();
        
        }

        public async Task<IEnumerable<Pie>> SearchPies(string searchQuery, int? categoryid)
        {

            //IQuerybale because in final request to database we only get <PageSize> amount of pies not all
            var pies = from p in _pieShopDbContext.Pies
                       select p;
            if(!string.IsNullOrEmpty(searchQuery))
            {
                pies = pies.Where(s => s.Name.ToLower().Contains(searchQuery.ToLower()) || s.ShortDescription.ToLower().Contains(searchQuery.ToLower())
                || s.LongDescription.ToLower().Contains(searchQuery.ToLower()));

            }
            if(categoryid != null)
            {
                pies = pies.Where(s => s.CategoryId == categoryid);
            }

            return await pies.ToListAsync();
        }

        public async Task<int> UpdatePieAsync(Pie pie)
        {
            var pieToUpdate = await _pieShopDbContext.Pies.FirstOrDefaultAsync(c => c.PieId == pie.PieId);
            if (pieToUpdate != null) 
            {
                pieToUpdate.CategoryId = pie.CategoryId;
                pieToUpdate.ShortDescription = pie.ShortDescription;
                pieToUpdate.LongDescription = pie.LongDescription;
                pieToUpdate.Price = pie.Price;
                pieToUpdate.AllergyInformation = pie.AllergyInformation;
                pieToUpdate.ImageThumbnailUrl = pie.ImageThumbnailUrl;
                pieToUpdate.ImageUrl = pie.ImageUrl;
                pieToUpdate.InStock = pie.InStock;
                pieToUpdate.IsPieOfTheWeek = pie.IsPieOfTheWeek;
                pieToUpdate.Name = pie.Name;

                _pieShopDbContext.Pies.Update(pieToUpdate);
                return await _pieShopDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The pie to update can't be found");
            }
        }
    }
}
