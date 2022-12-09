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
    public class OurVisionRepository:Repository<OurVision>,IOurVisionRepository
    {
        public OurVisionRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
