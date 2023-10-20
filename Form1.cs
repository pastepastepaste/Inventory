using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab_Exercise
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        List<object> showProductList;
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;
        public Form1()
        {
            InitializeComponent();
            showProductList = new List<object>();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String[] ListOfProductCategory =
                {"Beverages", "Bread/Bakery", "Canned/Jarred Goods", "Dairy", "Frozen Goods", "Meat", "Personal Care", "Other"};
            foreach (var item in ListOfProductCategory)
            {
                cbCategory.Items.Add(item);
            }
        }
        class NumberFormatException : Exception
        {
            public NumberFormatException(string varName) : base(varName) { }
        }
        class StringFormatException : Exception
        {
            public StringFormatException(string varName) : base(varName) { }
        }
        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string varName) : base(varName) { }
        }
        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                throw new StringFormatException("Valid Product Name");
            return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
                throw new NumberFormatException("Valid Quantity");
            return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                throw new CurrencyFormatException("Valid Price");
            return Convert.ToDouble(price);
        }

        private DataGridView GetGridViewProductList()
        {
            return gridViewProductList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch (NumberFormatException n) 
            {
                MessageBox.Show(n.Message); 
            }
            catch (StringFormatException s) 
            { 
                MessageBox.Show(s.Message); 
            }
            catch (CurrencyFormatException c) 
            { 
                MessageBox.Show(c.Message);
            }
            finally 
            { 

            }
        }
    }
 }

