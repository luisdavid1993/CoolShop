namespace CoolShop.CustomerInterface
{
    public interface ICustomer
    {
        public int Id { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }

        public bool Validete(out string errorMessage);
        //This method should not be here, because real world customer do not have actios like Clone
        public void Clone();
        //This method should not be here, because real world customer do not have actios like Revert
        public  void Revert(); // revert to old copy
    }
}