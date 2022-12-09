using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositories.Abstract
{
    public interface IPricingRepository:IRepository<Pricing>
    {

        IQueryable<Pricing> FilterbyTitle(string title);
        IQueryable<Pricing> FilterbyCreateAt(IQueryable<Pricing> aboutUs, DateTime? createAtStart, DateTime? createTimeEnd);
    }
}
