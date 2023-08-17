using CoolShop.CustomerHelpper;
using CoolShop.CustomerInterface;
using CoolShop.CustomerValidation;
using CoolShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.FactoryCustomer
{
    /// <summary>
    /// Version 2 con la palabra reservada Lazy
    /// </summary>
    internal static class FactoryWithLazyV2 //Simple factory
    {
        //Lazy Loading
        private static Lazy<Dictionary<string, CustomerBase>> cust =
              new Lazy<Dictionary<string, CustomerBase>>();

        public static CustomerBase Create(string customerType)
        {
            //Decorator pattern Implementation
            IValidation<ICustomer> leadValidation = new PhoneValidation(new CustomerValidationBasic());
            IValidation<ICustomer> custommerValidation = new BillValidation(
                                                        new PhoneValidation(
                                                        new CustomerValidationBasic()));


            cust.Value.Add("Customer", new Customer(custommerValidation, new DiscountSatSun(), new ExtraChargeSatSun(), "Customer")); 
            cust.Value.Add("Lead", new Customer(leadValidation, new DiscountNotAvalible(), new ExtraChargeNotAvalible(), "Lead"));
            return cust.Value[customerType]; //RIP; replace if with polymorphism
        }
    }
}
