using CoolShop.CustomerInterface;

namespace CoolShop.Domain
{
  

    public class Customer : CustomerBase
    {
        public Customer() 
        { }
        public Customer(IValidation<ICustomer> valida, string customerType) : base(valida)
        {
            CustomerType = customerType;
        }
        public override bool Equals(object? obj)
        {
           if(obj == null)
                return false;
           if(obj.GetHashCode() == this.GetHashCode())
                return true;
           return false;
        }
        public override int GetHashCode()
        {
            return $"{Id}{CustomerName}{CustomerType}{PhoneNumber}{Address}{BillAmount}{BillDate}".GetHashCode();
        }
        public override string ToString()
        {
            return $" {CustomerName} -- {CustomerType} -- {PhoneNumber} -- {Address}";
        }
    }

}