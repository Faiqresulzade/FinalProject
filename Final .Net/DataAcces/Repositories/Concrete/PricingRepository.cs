using Core.Entities;
using DataAcces.Contexts;
using DataAcces.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositories.Concrete
{
    public class PricingRepository:Repository<Pricing>,IPricingRepository
    {
        private readonly AppDbContext _appDbContext;

        public PricingRepository(AppDbContext appDbContext):base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<Pricing> FilterbyCreateAt(IQueryable<Pricing> pricing, DateTime? createAtStart, DateTime? createTimeEnd)
        {
            return pricing.Where(p => (createAtStart != null ? p.CreateAt >= createAtStart : true) && (createTimeEnd != null ? p.CreateAt <= createTimeEnd : true));
        }

        public IQueryable<Pricing> FilterbyTitle(string title)
        {
            return _appDbContext.PricingsComponent.Where(p => !string.IsNullOrEmpty(title) ? p.Title.Contains(title) : true);
        }
    }
}
