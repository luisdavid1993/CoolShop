using CoolShop.DalInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolShop.Domain;


namespace CoolShop.EntityFrameworkDAL
{

    //Desing Pattern =- Adapter Pattern (the same way to access to difference implementations)
    /// <summary>
    /// because ADO and Entity Framework is working with the same Repository (IDal) but difference implementation.
    /// </summary>
    /// <typeparam name="AnyType"></typeparam>
    // Entity Framework is the tree party incompatyble interface
    public class EFDalBase<AnyType> : DbContext, IRepository<AnyType> where AnyType : class
    {
        public EFDalBase():base("name=CoolShopConn") { }
        public void Add(AnyType obj)
        {
           Set<AnyType>().Add(obj); //In memory commit
        }

        public IEnumerable<AnyType> Get()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            SaveChanges();//pysical commit 
        }

        public IEnumerable<AnyType> Search()
        {
          return Set<AnyType>()
                .AsQueryable<AnyType>()
                .ToList<AnyType>();
        }

        public virtual void SetUnitOfWork(IUnitOfWork uow)
        {
            throw new NotImplementedException();
        }

        public void Update(AnyType obj)
        {
            throw new NotImplementedException();
        }
    }
}
