using CoolShop.CustomerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.CustomerValidation
{
    public class CustomerValidationBasic: IValidation<ICustomer>
    {
        public virtual bool Validate(ICustomer anyType, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(anyType.CustomerType))
                errorMessage += "CustomerType is required" + Environment.NewLine;
            if (string.IsNullOrWhiteSpace(anyType.CustomerName))
                errorMessage += "CustomerName is required" + Environment.NewLine;
           
            return errorMessage.Length == 0;
        }
    }

    //Desing Pattern =- Decorator pattern (Ad funcionalities by adding differences class)
    public class ValidationLinker : IValidation<ICustomer>
    {
        private IValidation<ICustomer> nextValidator;
        public ValidationLinker(IValidation<ICustomer> iValidator)
        {
            nextValidator = iValidator;
        }
        public virtual bool Validate(ICustomer anyType, out string errorMessage)
        {
           return nextValidator.Validate(anyType, out errorMessage);
        }
    }

    public class PhoneValidation : ValidationLinker
    {
        public PhoneValidation(IValidation<ICustomer> iValidator) : base(iValidator)
        {
        }

        public override bool Validate(ICustomer anyType, out string errorMessage)
        {
            base.Validate(anyType, out errorMessage);
            if (string.IsNullOrWhiteSpace(anyType.PhoneNumber))
                errorMessage += "PhoneNumber is required" + Environment.NewLine;

            return errorMessage.Length == 0;
        }
    }

    public class BillValidation : ValidationLinker
    {
        public BillValidation(IValidation<ICustomer> iValidator) : base(iValidator)
        {
        }

        public override bool Validate(ICustomer anyType, out string errorMessage)
        {
             base.Validate(anyType, out errorMessage);
            if (anyType.BillAmount <= 0)
                errorMessage += "BillAmount is required" + Environment.NewLine;
            return errorMessage.Length == 0;
        }
    }

    public class DateValidation : ValidationLinker
    {
        public DateValidation(IValidation<ICustomer> iValidator) : base(iValidator)
        {
        }

        public override bool Validate(ICustomer anyType, out string errorMessage)
        {
             base.Validate(anyType, out errorMessage);
            if (anyType.BillDate == default(DateTime))
                errorMessage += "BillDate is required" + Environment.NewLine;

            return errorMessage.Length == 0;
        }
    }

    public class AddressValidation : ValidationLinker
    {
        public AddressValidation(IValidation<ICustomer> iValidator) : base(iValidator)
        {
        }

        public override bool Validate(ICustomer anyType, out string errorMessage)
        {
            base.Validate(anyType, out errorMessage);
            if (string.IsNullOrWhiteSpace(anyType.Address))
                errorMessage += "Address is required" + Environment.NewLine;
            return errorMessage.Length == 0;
        }
    }


}
