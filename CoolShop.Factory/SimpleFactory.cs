using CoolShop.CustomerHelpper;
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
    public static class SimpleFactory //Simple factory (create objects)
    {

        private static Lazy<UnityContainer> container  = new Lazy<UnityContainer>();  //Lazy Loading (Just load at one time)

        public static CustomerBase Create(string customerType)
        {
            //Decorator pattern Implementation
            IValidation<ICustomer> leadValidation = new PhoneValidation(new CustomerValidationBasic());
            IValidation<ICustomer> basicValidation = new CustomerValidationBasic();
            IValidation<ICustomer> deliveryValidation = new AddressValidation(new CustomerValidationBasic());
            IValidation<ICustomer> customerValidation =new BillValidation( 
                                                        new PhoneValidation(
                                                        new CustomerValidationBasic()));


            container.Value.RegisterType<CustomerBase, Customer>("Customer",new InjectionConstructor(customerValidation, new DiscountSatSun(), new ExtraChargeSatSun(), "Customer"));
            container.Value.RegisterType<CustomerBase, Customer>("Lead", new InjectionConstructor(leadValidation, new DiscountNotAvalible(), new ExtraChargeNotAvalible(), "Lead"));
            container.Value.RegisterType<CustomerBase, Customer>("SelfService", new InjectionConstructor(basicValidation, new DiscountBillAmount(), null, "SelfService"));
            container.Value.RegisterType<CustomerBase, Customer>("Delivery", new InjectionConstructor(deliveryValidation, new DiscountBillAmount(), new ExtraChargeSatSun(), "Delivery"));
            return container.Value.Resolve<CustomerBase>(customerType);  //Desing Pattern =- RIP; (replace if with polymorphism)
        }
    }
}
