using BelajarCRUDWPF.Model;
using BelajarCRUDWPF.MyContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BelajarCRUDWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        myContext con = new myContext();
        int idsupp;

        public void empty_txb_supp()
        {
            foreach (Control ctl in containersupplier.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
            }
        }

        public void empty_txb_item()
        {
            foreach (Control ctl in containeritem.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            tblsupplier.ItemsSource = con.Suppliers.ToList();
            tblitems.ItemsSource = con.Items.ToList();

            cb_supplier.ItemsSource = con.Suppliers.ToList();
        }

        private void btninsert_Click(object sender, RoutedEventArgs e)
        {
            if(txb_name_supp.Text == "")
            {
                MessageBox.Show("Name cannot be empty");
            }
            else if(txb_address_supp.Text == ""){
                MessageBox.Show("Address cannot be empty");
            }
            else
            {
                var input_supp = new Supplier(txb_name_supp.Text, txb_address_supp.Text); // new nama_model();
                con.Suppliers.Add(input_supp);
                con.SaveChanges();
                tblsupplier.ItemsSource = con.Suppliers.ToList();
                cb_supplier.ItemsSource = con.Suppliers.ToList();
                MessageBox.Show("Success Insert Data!");

                empty_txb_supp();
            }
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            if (txb_name_supp.Text == "")
            {
                MessageBox.Show("Name cannot be empty");
            }
            else if (txb_address_supp.Text == "")
            {
                MessageBox.Show("Address cannot be empty");
            }
            else
            {
                int id = Convert.ToInt32(txb_id_supp.Text);
                var myid = con.Suppliers.Where(s => s.Id == id).FirstOrDefault(); // menginisiasi table supplier menjadi S
                myid.Name = txb_name_supp.Text;
                myid.Address = txb_address_supp.Text;

                con.SaveChanges();
                tblsupplier.ItemsSource = con.Suppliers.ToList();
                MessageBox.Show("Succes Update Data!");

                empty_txb_supp();
            }
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure delete this data?",
               "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int id = Convert.ToInt32(txb_id_supp.Text);
                var myid = con.Suppliers.Where(s => s.Id == id).FirstOrDefault(); // menginisiasi table supplier menjadi S

                con.Suppliers.Remove(myid);
                con.SaveChanges();
                tblsupplier.ItemsSource = con.Suppliers.ToList();
                MessageBox.Show("Succes Delete Data!");

                empty_txb_supp();
            }
            else
            {
                
            }
        }

        private void tblsupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = tblsupplier.SelectedItem;
            if(data == null)
            {
                tblsupplier.Items.Refresh();
            }
            else
            {
                string id = (tblsupplier.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text; txb_id_supp.Text = id;
                string name = (tblsupplier.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text; txb_name_supp.Text = name;
                string address = (tblsupplier.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text; txb_address_supp.Text = address;
            }
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Do you want to close this window?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        // ITEM
        private void cbsupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idsupp = Convert.ToInt32(cb_supplier.SelectedValue.ToString());
        }

        private void btninsertitem_Click(object sender, RoutedEventArgs e)
        {
            if (txb_name_item.Text == "")
            {
                MessageBox.Show("Name cannot be empty");
            }
            else if (txb_price_item.Text == "")
            {
                MessageBox.Show("Price cannot be empty");
            }
            else if (txb_stock_item.Text == "")
            {
                MessageBox.Show("Stock cannot be empty");
            }
            else if (cb_supplier.Text == "")
            {
                MessageBox.Show("Supplier cannot be empty");
            }
            else
            {
                var supp_id = con.Suppliers.Where(s => s.Id == idsupp).FirstOrDefault();
                var input_item = new Item(txb_name_item.Text, int.Parse(txb_price_item.Text), int.Parse(txb_stock_item.Text), supp_id);

                con.Items.Add(input_item);
                con.SaveChanges();
                tblitems.ItemsSource = con.Items.ToList();
                MessageBox.Show("Success Insert Data!");

                empty_txb_item();
            }
        }

        private void btnupdateitem_Click(object sender, RoutedEventArgs e)
        {
            if (txb_name_item.Text == "")
            {
                MessageBox.Show("Name cannot be empty");
            }
            else if (txb_price_item.Text == "")
            {
                MessageBox.Show("Price cannot be empty");
            }
            else if (txb_stock_item.Text == "")
            {
                MessageBox.Show("Stock cannot be empty");
            }
            else if (cb_supplier.Text == "")
            {
                MessageBox.Show("Supplier cannot be empty");
            }
            else
            {
                var supp_id = con.Suppliers.Where(s => s.Id == idsupp).FirstOrDefault();
                int id = Convert.ToInt32(txb_id_item.Text);
                var myid = con.Items.Where(s => s.Id == id).FirstOrDefault(); // menginisiasi table supplier menjadi S
                myid.Name = txb_name_item.Text;
                myid.Price = int.Parse(txb_price_item.Text);
                myid.Stock = int.Parse(txb_stock_item.Text);
                myid.Supplier = supp_id;

                con.SaveChanges();
                tblitems.ItemsSource = con.Items.ToList();
                MessageBox.Show("Succes Update Data!");

                empty_txb_item();
            }
        }

        private void tblitems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = tblitems.SelectedItem;
            if (data == null)
            {
                tblitems.Items.Refresh();
            }
            else
            {
                string id = (tblitems.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text; txb_id_item.Text = id;
                string name = (tblitems.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text; txb_name_item.Text = name;
                string stock = (tblitems.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text; txb_price_item.Text = stock;
                string item = (tblitems.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text; txb_stock_item.Text = item;
                string supplier = (tblitems.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text; cb_supplier.Text = supplier;
            }
        }
    }
}
