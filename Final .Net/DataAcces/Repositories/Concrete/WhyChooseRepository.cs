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
    public class WhyChooseRepository:Repository<WhyChoose>,IWhyChooseRepository
    {
        public WhyChooseRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
