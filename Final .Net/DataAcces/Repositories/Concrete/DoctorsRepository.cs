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
    public class DoctorsRepository:Repository<Doctors>,IDoctorsRepository
    {
        public DoctorsRepository(AppDbContext appDbContext):base(appDbContext)
        {

        }
    }
}
