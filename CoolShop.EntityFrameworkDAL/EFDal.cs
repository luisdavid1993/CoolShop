using CoolShop.CustomerInterface;
using CoolShop.Domain;
using System.Data.Entity;

namespace CoolShop.EntityFrameworkDAL
{
    public class EFDal: EFDalBase<CustomerBase>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerBase>().ToTable("Client");
            modelBuilder.Entity<CustomerBase>()
                .Map<Customer>(m => m.Requires("CustomerType").HasValue("Customer"))
                .Map<Customer>(m => m.Requires("CustomerType").HasValue("Lead"));
            modelBuilder.Entity<CustomerBase>().Ignore(t => t.CustomerType);
        }
    }
}