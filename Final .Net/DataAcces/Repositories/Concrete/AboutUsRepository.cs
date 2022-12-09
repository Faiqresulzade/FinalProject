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
    public class AboutUsRepository : Repository<AboutUs>, IAboutUsRepository
    {
        private readonly AppDbContext _appDbContext;

        public AboutUsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
       
        public IQueryable<AboutUs> FilterbyTitle(string title)
        {
            return  _appDbContext.GetAbouts.Where(p => !string.IsNullOrEmpty(title) ? p.Title.Contains(title) : true);
        }
        public IQueryable<AboutUs> FilterbyCreateAt(IQueryable<AboutUs> aboutUs, DateTime? createAtStart, DateTime? createTimeEnd)
        {
            return aboutUs.Where(p => (createAtStart != null ? p.CreateAt >= createAtStart : true) && (createTimeEnd != null ? p.CreateAt <= createTimeEnd : true));
        }

    }
}
