using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<OurVision> OurVisions { get; set; }
        public DbSet<AboutUs> GetAbouts { get; set; }
        public DbSet<MedicalDepartments> MedicalDepartments { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<WhyChoose> WhyChoose { get; set; }
        public DbSet<LastestNews> Lastests { get; set; }
        public DbSet<Pricing> PricingsComponent { get; set; }
    }
}
