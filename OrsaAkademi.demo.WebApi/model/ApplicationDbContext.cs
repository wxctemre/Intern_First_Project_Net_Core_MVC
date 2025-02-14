using Microsoft.EntityFrameworkCore;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.WebApi;
namespace OrsaAkademi.demo.WebApi.model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        
        

        }
        public DbSet<Personeller> personellers { get; set; }
        public DbSet<PersonelMedyalar> PersonelMedyalar { get; set; }
        public DbSet<MedyaKutuphanesi> MedyaKutuphanesi { get; set; }
        public DbSet<Okullar> okullar { get; set; }
        public DbSet<ParamOkullar> paramokullar { get; set; }
        public DbSet<PersonelOkulMedyalariiliski> PersonelEgitimId { get; set; }





    }
}
