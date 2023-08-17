using CoolShop.CustomerInterface;

namespace CoolShop.Domain
{
  

    public class Customer : CustomerBase
    {
        public Customer() 
        { }
        public Customer(IValidation<ICustomer> valida, IDiscount discount, IExtraCharge extraCharge, string customerType) : base(valida, discount, extraCharge)
        {
            CustomerType = customerType;
        }
       
    }

}