using CoolShop.CustomerInterface;
using CoolShop.DalInterface;
using CoolShop.FactoryDAL;
using static CoolShop.Console.FrmCustomer;

namespace CoolShop.Console
{

    public partial class FrmCustomer
    {
        // Mediator Pattern
        private CustomerMediator customerMediator = new CustomerMediator();
        private CustomerBase cust;
        private IRepository<CustomerBase> Idal;
        public FrmCustomer()
        {
            DataGridView dtgGridCustomer = new DataGridView();
            customerMediator.Register(cust);
            customerMediator.Register(Idal);
            customerMediator.Register(dtgGridCustomer);
            foreach (TextBox t in Control.OfType<TextBox>())
            {
                customerMediator.Register(t);
            }
            foreach (ComboBox t in Control.OfType<ComboBox>())
            {
                customerMediator.Register(t);
            }

        }
    }
    internal class CustomerMediator  //Design pattern :- Mediator Pattern (Interraction with diference layer. Middle point)
    {
        private CustomerBase cust;
        private IRepository<CustomerBase> Idal;

        private DataGridView dtgGridCustomer;
        private Dictionary<string, Control> UIControls = new Dictionary<string, Control>();
        public void Register(CustomerBase _cust)
        {
            cust = _cust;
        }
        public void Register(IRepository<CustomerBase> _dal)
        {
            Idal = _dal;
        }
        public void Register(TextBox txt)
        {
            UIControls.Add(txt.Name, txt);
        }
        public void Register(ComboBox cmb)
        {
            UIControls.Add(cmb.Name, cmb);
        }
        public void Register(DataGridView grid)
        {
            dtgGridCustomer = grid;
        }

        public void Load()
        {
            ComboBox comb = (ComboBox)UIControls["DalLayer"];
            comb.Items.Add("ADODal");
            comb.Items.Add("EFDal");
            comb.SelectedIndex = 0;
            Idal = FactoryDAL<IRepository<CustomerBase>>.Create(comb.Text);
            LoadGrid();
            ClearCustomer();
        }



        public void LoadGridInMemory()
        {
            dtgGridCustomer.DataSource = null;
            IEnumerable<CustomerBase> custs = Idal.Get(); //inmemory
            dtgGridCustomer.DataSource = custs;

        }

        private void ClearCustomer()
        {
            throw new NotImplementedException();
        }

        private void LoadGrid()
        {
            throw new NotImplementedException();
        }
    }
    #region Class and Properties for Mop objects

    internal class ComboBox : Control
    {
        public int SelectedIndex { get; internal set; }
        public string Name { get; set; }
        public List<object> Items { get; internal set; }
        public string Text { get; internal set; }
    }

    internal class TextBox : Control
    {
        public string Name { get; set; }
    }

    internal class Control
    {
        internal static IEnumerable<T> OfType<T>()
        {
            throw new NotImplementedException();
        }
    }

    internal class DataGridView
    {
        public object DataSource { get; internal set; }
    }

    #endregion
}
