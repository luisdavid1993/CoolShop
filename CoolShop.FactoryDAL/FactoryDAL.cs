using CoolShop.AdoDAL;
using CoolShop.CustomerInterface;
using CoolShop.DalInterface;
using CoolShop.EntityFrameworkDAL;
using System.Configuration;
using Unity;
using Unity.Injection;

namespace CoolShop.FactoryDAL
{
    public static class FactoryDAL<AnyType>
    {
        // I can not use ICustomer because of Entity Framework does not work with Interfaces
        private static Lazy<UnityContainer> container = new Lazy<UnityContainer>();
        public static AnyType Create(string dalType)
        {
            container.Value.RegisterType<IRepository<CustomerBase>, CustommerDAL>("AdoDAL");
            container.Value.RegisterType<IRepository<CustomerBase>, EFDal>("EF");
            container.Value.RegisterType<IUnitOfWork, UnitOfWorkDAL>("AdoUOW");
            return container.Value.Resolve<AnyType>(dalType);
        }
    }
}