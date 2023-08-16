using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.DalInterface
{
    public abstract class CommonDAL<AnyType> : IRepository<AnyType>
    {
        protected List<AnyType> _items;

        protected CommonDAL()
        {
            _items = new List<AnyType>();
        }
        public virtual void Add(AnyType obj)
        {
            if(_items.Count == 0)
                _items.Add(obj);
            foreach (var item in _items)
            {
                if(!item.Equals(obj))
                    _items.Add(obj);

            }
            
        }

        public IEnumerable<AnyType> Get()
        {
            return _items;
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<AnyType> Search()
        {
            throw new NotImplementedException();
        }

        public virtual void SetUnitOfWork(IUnitOfWork uow)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(AnyType obj)
        {
            throw new NotImplementedException();
        }
    }
}
