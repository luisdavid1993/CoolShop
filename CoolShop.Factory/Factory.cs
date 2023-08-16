using CoolShop.CustomerInterface;
using CoolShop.CustomerValidation;
using CoolShop.Domain;
using Unity;
using Unity.Injection;

namespace CoolShop.FactoryCustomer
{
    /// <summary>
    /// Factory con libreria UnityContainer y palabra reservada Lazy
    /// </summary>
    public static class Factory //Desing Pattern =- Simple factory (create objects)
    {

        private static Lazy<UnityContainer> container  = new Lazy<UnityContainer>();  //Desing Pattern =- Lazy Loading (Just load at one time)

        public static CustomerBase Create(string customerType)
        {
            //Decorator pattern Implementation
            IValidation<ICustomer> leadValidation = new PhoneValidation(new CustomerValidationBasic());
            IValidation<ICustomer> basicValidation = new CustomerValidationBasic();
            IValidation<ICustomer> deliveryValidation = new AddressValidation(new CustomerValidationBasic());
            IValidation<ICustomer> customerValidation =new BillValidation( 
                                                        new PhoneValidation(
                                                        new CustomerValidationBasic()));


            container.Value.RegisterType<CustomerBase, Customer>("Customer",new InjectionConstructor(customerValidation, "Customer"));
            container.Value.RegisterType<CustomerBase, Customer>("Lead", new InjectionConstructor(leadValidation,"Lead"));
            container.Value.RegisterType<CustomerBase, Customer>("SelfService", new InjectionConstructor(basicValidation, "SelfService"));
            container.Value.RegisterType<CustomerBase, Customer>("Delivery", new InjectionConstructor(deliveryValidation, "Delivery"));
            return container.Value.Resolve<CustomerBase>(customerType);  //Desing Pattern =- RIP; (replace if with polymorphism)
        }
    }
}
