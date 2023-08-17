namespace CoolShop.CustomerInterface
{
    public interface ICustomer
    {
        int Id { get; set; }
        string CustomerType { get; set; }
        string CustomerName { get; set; }
        string PhoneNumber { get; set; }
        decimal BillAmount { get; set; }
        DateTime BillDate { get; set; }
        string Address { get; set; }
        bool Validete(out string errorMessage);
        //This method should not be here, because real world customer do not have actios like Clone
        void Clone();
        //This method should not be here, because real world customer do not have actios like Revert
        void Revert(); // revert to old copy

        decimal ActualCost(); //ActualCost = BillAmount - discount + extra charge 
    }
}