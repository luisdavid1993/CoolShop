using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolShop.CustomerInterface
{
    public class CustomerBase : ICustomer
    {

        private IValidation<ICustomer> validatios;

        private ICustomer oldCopy; //Desing Pattern =- Memento Pattern (revert back)
        [Key]
        public int Id { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }

        public CustomerBase()
        { 
        
        }
        public CustomerBase(IValidation<ICustomer> valida)
        {
            this.validatios = valida;
        }

        public bool Validete(out string errorMessage)
        {
            return validatios.Validate(this, out errorMessage);
        }

        public void Clone()
        {
            //cloning this object BY REFERENCE, But with the MemberwiseClone the oldcopy COPY THIS BY VALUE TYPE
            //in other words it create a new object
            oldCopy = (ICustomer)this.MemberwiseClone();  //Desing Pattern =- Prototype Pattern (Clone an object). shallow copy
        }

        public void Revert()
        {
            this.CustomerType= oldCopy.CustomerType;
            this.CustomerName = oldCopy.CustomerName;
            this.PhoneNumber = oldCopy.PhoneNumber;
            this.BillAmount = oldCopy.BillAmount;
            this.BillDate = oldCopy.BillDate;
            this.Address = oldCopy.Address;
        }
    }
}
