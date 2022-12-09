using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositories.Abstract
{
    public interface IAboutUsRepository:IRepository<AboutUs>
    {
        IQueryable<AboutUs> FilterbyTitle(string title);
        IQueryable<AboutUs> FilterbyCreateAt(IQueryable<AboutUs> aboutUs, DateTime? createAtStart, DateTime? createTimeEnd);
    }
}
