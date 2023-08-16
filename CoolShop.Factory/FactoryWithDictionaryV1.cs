using CoolShop.CustomerInterface;
using CoolShop.CustomerValidation;
using CoolShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.FactoryCustomer
{
    /// <summary>
    /// Version 1 solo con un diccionario
    /// </summary>
    internal static class FactoryWithDictionaryV1 //Simple factory
    {
        private static Dictionary<string, CustomerBase> cust =
              new Dictionary<string, CustomerBase>();

        public static CustomerBase Create(string customerType)
        {
            //Decorator pattern Implementation
            IValidation<ICustomer> leadValidation = new PhoneValidation(new CustomerValidationBasic());
            IValidation<ICustomer> custommerValidation = new BillValidation(
                                                        new PhoneValidation(
                                                        new CustomerValidationBasic()));

            //Lazy Loading
            if (cust.Count == 0)
            {
                cust.Add("Customer", new Customer(custommerValidation, "Customer"));
                cust.Add("Lead", new Customer(leadValidation, "Lead"));
            }

            return cust[customerType]; //RIP; replace if with polymorphism
          
        }
    }
}
