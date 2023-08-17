using CoolShop.CustomerHelpper;
using CoolShop.CustomerInterface;
using CoolShop.CustomerValidation;
using CoolShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Injection;
using Unity;

namespace CoolShop.CustomerFactory
{

    // Customer object = Validation + Discount + Extra
    //Design pattern :- Factory Method  (The Factory Method defines an interface for creating objects, 
    //but lets subclasses decide which classes to instantiate.)
    public class FactoryLookUp 
    {
        private static Lazy<UnityContainer> container = new Lazy<UnityContainer>();  //Desing Pattern =- Lazy Loading (Just load at one time)
        public static CustomerBase Create(string customerType)
        {
  
            container.Value.RegisterType<FactoryMethodBase, FactoryCustomer>("Customer");
            container.Value.RegisterType<FactoryMethodBase, FactoryLead>("Lead");
            container.Value.RegisterType<FactoryMethodBase, FactorySelfService>("SelfService");
            container.Value.RegisterType<FactoryMethodBase, FactoryHomeDelivery>("Delivery");
            FactoryMethodBase factoryMethodBase = container.Value.Resolve<FactoryMethodBase>(customerType);  //Desing Pattern =- RIP; (replace if with polymorphism)
            return factoryMethodBase.CreateCustomer();
        }

    }
    internal class FactoryCustomer : FactoryMethodBase
    {
        public FactoryCustomer()
        {
            _customerType = "Customer";
        }
        public override IValidation<ICustomer> CreateValidation()
        {
            return  new AddressValidation(
                    new BillValidation(
                    new PhoneValidation(
                    new CustomerValidationBasic())));
        }

        public override IDiscount CreateDiscount()
        {
            return new DiscountSatSun();
        }
    }

    internal class FactoryHomeDelivery : FactoryMethodBase
    {
        public FactoryHomeDelivery()
        {
            _customerType = "HomeDelivery";
        }

        public override IValidation<ICustomer> CreateValidation()
        {
            return new AddressValidation(new CustomerValidationBasic());
        }
    }
    internal class FactorySelfService : FactoryMethodBase
    {
        public FactorySelfService()
        {
            _customerType = "SelfService";
        }

        public override IExtraCharge CreateExtraCharge()
        {
            return null;
        }
    }

    internal class FactoryLead : FactoryMethodBase
    {
        public FactoryLead()
        {
            _customerType = "Lead";
        }
        public override IDiscount CreateDiscount()
        {
            return new DiscountNotAvalible();
        }
        public override IExtraCharge CreateExtraCharge()
        {
            return new ExtraChargeNotAvalible();
        }
        public override IValidation<ICustomer> CreateValidation()
        {
            return new PhoneValidation(new CustomerValidationBasic());
        }
    }



    /// <summary>
    /// Base clase to inheritance and create all customer types
    /// </summary>
    internal class FactoryMethodBase
    {
        protected string _customerType = string.Empty;
        public virtual IValidation<ICustomer> CreateValidation()
        {
            return new CustomerValidationBasic();
        }
        public virtual IDiscount CreateDiscount()
        {
            return new DiscountBillAmount();
        }
        public virtual IExtraCharge CreateExtraCharge()
        {
            return new ExtraChargeSatSun();
        }

        public virtual CustomerBase CreateCustomer()
        {
            return new Customer(CreateValidation(), CreateDiscount(), CreateExtraCharge(), _customerType);
        }
    }
}
