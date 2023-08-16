using CoolShop.AdoDAL;
using CoolShop.CustomerInterface;
using CoolShop.DalInterface;
using CoolShop.Domain;
using CoolShop.FactoryCustomer;
using CoolShop.FactoryDAL;
using System.Data.Common;
using System.Transactions;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");


        //TestNomalImplemantationWithAdoDAL();
        //TesUnitOfWork(); ////unit of work handle Transaction
        //TesTransactionScope(); ////  TransactionScope handle Transaction
        // CallMethodsForInsertManyRecords();

        CustomerBase customer = CallReverseMethod();


    }

    private static CustomerBase CallReverseMethod()
    {
        CustomerBase client = Factory.Create("Customer");
        client.Id = 2;
        client.CustomerName = "Maxi";
        client.PhoneNumber = "3213699622";
        client.BillAmount = 500;
        client.BillDate = DateTime.Now;
        client.Address = "Cra 7F #161-36-85";

        UpdateCustomer(client);

        CancelCustomerEdition(client);

        return client;
    }

    /// <summary>
    /// revert customers values 
    /// </summary>
    /// <param name="customer"></param>
    private static void CancelCustomerEdition(CustomerBase customer)
    {
        customer.Revert();
        Console.WriteLine("Revert Method");
        Console.WriteLine(customer.ToString());
    }

    /// <summary>
    /// clone one customer before update
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="dal"></param>
    private static void UpdateCustomer(CustomerBase customer) 
    {
        customer.Clone();
        customer.CustomerName = "Updated Client";
        Console.WriteLine("Update Method");
        Console.WriteLine(customer.ToString());
    }

    private static void CallMethodsForInsertManyRecords()
    {
        IRepository<CustomerBase> dal = FactoryDAL<IRepository<CustomerBase>>.Create("AdoDAL");
        IEnumerable<CustomerBase> lstCustomers = AddCustomerInMemoryAndSave(dal);
        foreach (var item in lstCustomers)
        {
            Console.WriteLine(item.ToString());
        }
        dal.Save();
        Console.WriteLine("saved");

        Console.ReadLine();
    }
    /// <summary>
    /// add object CustomerBase in memory
    /// </summary>
    /// <param name="customerType"></param>
    /// <returns></returns>
    private static IEnumerable<CustomerBase> AddCustomerInMemoryAndSave(IRepository<CustomerBase> dal)
    {
        CustomerBase client = Factory.Create("Customer");
        client.Id = 2;
        client.CustomerName = "Conie";
        client.PhoneNumber = "3213699622";
        client.BillAmount = 500;
        client.BillDate = DateTime.Now;
        client.Address = "Cra 7F #161-36-85";


        dal.Add(client);


        CustomerBase client2 = Factory.Create("Lead");
        client2.CustomerName = "Luis David";
        client2.PhoneNumber = "3213699622";
        client2.BillAmount = 500;
        client2.BillDate = DateTime.Now;
        client2.Address = "Cra 7F #161-36";

        dal.Add(client2);

        return dal.Get();
    }

    private static void TestNomalImplemantationWithAdoDAL()
    {

        CustomerBase client = Factory.Create("Customer");

        client.Id = 2;
        client.CustomerName = "Conie";
        client.PhoneNumber = "3213699622";
        client.BillAmount = 500;
        client.BillDate = DateTime.Now;
        client.Address = "Cra 7F #161-36-85";



        IRepository<CustomerBase> dal = FactoryDAL<IRepository<CustomerBase>>.Create("AdoDAL");
        dal.Add(client);
        dal.Save();


        Console.WriteLine(client);

        Console.WriteLine("Client List");
        IEnumerable<CustomerBase> clients = dal.Search();
        foreach (var item in clients)
        {

            Console.WriteLine($"client : {item.CustomerName} {item.PhoneNumber} {item.Address}");
        }


    }

    private static void TesUnitOfWork()
    {
        IUnitOfWork uow = FactoryDAL<IUnitOfWork>.Create("AdoUOW");

        try
        {



            CustomerBase client = Factory.Create("Customer");
            client.CustomerName = "client 08";
            client.PhoneNumber = "3143157981";
            client.BillAmount = 500;
            client.BillDate = DateTime.Now;
            client.Address = "Cra 7F #161-36";


            IRepository<CustomerBase> dal = FactoryDAL<IRepository<CustomerBase>>.Create("AdoDAL");
            dal.SetUnitOfWork(uow);
            dal.Add(client);
            dal.Save();

            CustomerBase client2 = Factory.Create("Customer");
            client2.CustomerName = null;
            client2.PhoneNumber = "HOLA COMO ";
            client2.BillAmount = 500;
            client2.BillDate = DateTime.Now;
            client2.Address = "Cra 7F #161-36";

            IRepository<CustomerBase> dal2 = FactoryDAL<IRepository<CustomerBase>>.Create("AdoDAL");
            dal2.SetUnitOfWork(uow);
            dal2.Add(client2);
            dal2.Save();


            uow.Committ(); // Unit of Work Committ

            Console.WriteLine($" Se inserto ");
            Console.WriteLine(client.ToString());
            Console.WriteLine(client2.ToString());


        }
        catch (Exception ex)
        {
            uow.RollBack();
            Console.WriteLine(ex.Message);
        }
    }


    private static void TesTransactionScope()
    {

        try
        {

            int testCompareWithHasCode = 0;
            int testCompareWithHasCode2 = 0;
            DateTime current = DateTime.Now;

            using (TransactionScope scope = new TransactionScope())
            {
                CustomerBase client = Factory.Create("Customer");
                client.CustomerName = "Luis David";
                client.PhoneNumber = "3213699622";
                client.BillAmount = 500;
                client.BillDate = default(DateTime);
                client.Address = "Cra 7F #161-36";

                testCompareWithHasCode = client.GetHashCode();
                IRepository<CustomerBase> dal = FactoryDAL<IRepository<CustomerBase>>.Create("AdoDAL");
                dal.Add(client);
                dal.Save();




                CustomerBase client2 = Factory.Create("Lead");
                client2.CustomerName = "Mariangeles";
                client2.PhoneNumber = "3143157981";
                client2.BillAmount = 500;
                client2.BillDate = current;
                client2.Address = "Cra 7F #161-36";



                testCompareWithHasCode2 = client2.GetHashCode();

                if (!client.Equals(client2))//if (testCompareWithHasCode != testCompareWithHasCode2)
                {

                    IRepository<CustomerBase> dal2 = FactoryDAL<IRepository<CustomerBase>>.Create("AdoDAL");
                    dal2.Add(client2);
                    dal2.Save();
                }

                scope.Complete();

                Console.WriteLine($" Se inserto ");
                Console.WriteLine(client.ToString());
                Console.WriteLine(client2.ToString());

            }
            IRepository<CustomerBase> dalSeach = FactoryDAL<IRepository<CustomerBase>>.Create("AdoDAL");
            Console.WriteLine("Client List");
            IEnumerable<CustomerBase> clients = dalSeach.Search();
            foreach (var item in clients)
            {

                Console.WriteLine($"client : {item.CustomerType} {item.CustomerName} {item.PhoneNumber} {item.Address}");
            }
        }
        catch (TransactionAbortedException tex)
        {
            Console.WriteLine("TransactionAbortedException Message: {0}", tex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}
