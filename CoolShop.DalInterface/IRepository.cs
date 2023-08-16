namespace CoolShop.DalInterface
{
    //Desing Pattern =- Repository Pattern / Generic Repository Pattern
    public interface IRepository<AnyType>
    {
        void SetUnitOfWork(IUnitOfWork uow);
        void Add(AnyType obj); // in memory
        void Update(AnyType obj); // in memory
        IEnumerable<AnyType> Search(); // From DB  //Desing Pattern =- Iteretor Pattern (Allow just Iteratios). 
        //It does not allow ADD, REMOVE method. IEnumerable or IEnumerator
        //IEnumerable is an interface defining a single method GetEnumerator() that returns an IEnumerator interface
        //IEnumerator has two methods MoveNext and Reset. It also has a property called Current.
        IEnumerable<AnyType> Get(); // from memory //Iteretor Pattern. It does not allow ADD, REMOVE method. IEnumerable or IEnumerator
        void Save(); // pysical save - database
    }
}