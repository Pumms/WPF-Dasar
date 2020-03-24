using BelajarCRUDWPF.Model;
using BelajarCRUDWPF.MyContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UCTbItem.xaml
    /// </summary>
    public partial class UCTbItem : UserControl
    {
        myContext con = new myContext();
        int idsupp;

        public UCTbItem()
        {
            InitializeComponent();
            tblitems.ItemsSource = con.Items.ToList();
            cb_supplier.ItemsSource = con.Suppliers.ToList();
        }

        private void cbsupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idsupp = Convert.ToInt32(cb_supplier.SelectedValue.ToString());
        }

        //DataGrid Klik
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

        //Save
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txb_id_item.Text == "")
            {
                // Input Function
                var nama = txb_name_item.Text;
                string pattern = "^[a-zA-Z0-9]";

                Regex rx = new Regex(pattern);
                bool result = rx.IsMatch(nama);

                if (nama == "")
                {
                    MessageBox.Show("Name cannot be empty");
                    txb_name_item.Focus();
                }
                else if (!result)
                {
                    MessageBox.Show("Format Name Item invalid");
                    txb_name_item.Focus();
                }
                else if (txb_price_item.Text == "")
                {
                    MessageBox.Show("Price cannot be empty");
                    txb_price_item.Focus();
                }
                else if (txb_stock_item.Text == "")
                {
                    MessageBox.Show("Stock cannot be empty");
                    txb_price_item.Focus();
                }
                else if (cb_supplier.Text == "")
                {
                    MessageBox.Show("Supplier cannot be empty");
                    cb_supplier.Focus();
                }
                else
                {
                    try
                    {
                        if (MessageBox.Show("Are you sure, Insert this data?",
                       "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            var supp_id = con.Suppliers.Where(s => s.Id == idsupp).FirstOrDefault();
                            var input_item = new Item(txb_name_item.Text, int.Parse(txb_price_item.Text), int.Parse(txb_stock_item.Text), supp_id);

                            con.Items.Add(input_item);
                            con.SaveChanges();
                            tblitems.ItemsSource = con.Items.ToList();
                            MessageBox.Show("Success Insert Data!");

                            empty_txb_item();
                        }
                        else
                        {
                            //do nothing
                        }
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("Erro Insert Data" + ex);
                    }
                }
            }

            else if (txb_id_item != null)
            {
                //Update Function
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
                    //MessageBox.Show("Supplier cannot be empty");
                }
                else
                {
                    try
                    {
                        if (MessageBox.Show("Are you sure Update this data?",
                        "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                        else
                        {
                            //do nothing
                        }
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("Erro Update Data" + ex);
                    }
                }
            }
        }

        //Hapus
        private void HapusRow(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure delete this data?",
               "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    var data = tblitems.SelectedItem;
                    string id_datagrid = (tblitems.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                    int id = int.Parse(id_datagrid);
                    var myid = con.Items.Where(s => s.Id == id).FirstOrDefault();

                    con.Items.Remove(myid);
                    con.SaveChanges();
                    tblitems.ItemsSource = con.Items.ToList();
                    MessageBox.Show("Succes Delete Data!");

                    empty_txb_item();
                }
                catch (Exception)
                {
                    //MessageBox.Show("Error Delete Data" + ex);
                }
            }
            else
            {
                //do nothing
            }
        }

        //Refresh
        private void refresh_Btn_Click(object sender, RoutedEventArgs e)
        {
            empty_txb_item();
            tblitems.ItemsSource = con.Items.ToList();
        }

        //Search
        private void Search_Btn(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Item> ListItem = new List<Item>();
                int intvalue;

                if (Search_box.Text == "")
                {
                    tblitems.ItemsSource = con.Suppliers.ToList();
                }

                foreach (Item item in con.Items.ToList())
                {
                    if (item.Name.ToLower().Contains(Search_box.Text.ToLower()))
                    {
                        ListItem.Add(item);
                    }
                    else if (int.TryParse(Search_box.Text, out intvalue))
                    {
                        if (item.Id.Equals(Convert.ToInt32(Search_box.Text)))
                        {
                            ListItem.Add(item);
                        }
                        if (item.Price.Equals(Convert.ToInt32(Search_box.Text)))
                        {
                            ListItem.Add(item);
                        }
                        if (item.Stock.Equals(Convert.ToInt32(Search_box.Text)))
                        {
                            ListItem.Add(item);
                        }
                    }
                    else if (item.Supplier.Name.ToLower().Contains(Search_box.Text.ToLower()))
                    {
                        ListItem.Add(item);
                    }
                }

                tblitems.ItemsSource = ListItem.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        public void empty_txb_item()
        {
            foreach (Control ct in container_item.Children)
            {
                try
                {
                    if (ct.GetType() == typeof(TextBox))
                    {
                        ((TextBox)ct).Text = String.Empty;
                    }
                    cb_supplier.Items.Clear();
                    cb_supplier.SelectedIndex = -1;
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
