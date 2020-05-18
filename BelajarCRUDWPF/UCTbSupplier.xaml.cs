using BelajarCRUDWPF.Model;
using BelajarCRUDWPF.MyContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for UCTbSupplier.xaml
    /// </summary>
    public partial class UCTbSupplier : UserControl
    {
        myContext con = new myContext();
        int id_role;

        public UCTbSupplier()
        {
            InitializeComponent();
            tblsupplier.ItemsSource = con.Suppliers.ToList();
            cb_role.ItemsSource = con.Roles.ToList();
        }

        //Send Email
        public void Sendmail(string EmailTujuan, string name, string password)
        {
            string to = EmailTujuan;
            string from = "web.tester1998@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            message.Subject = "Password for Login " + date;
            string isipesan = "Hi " + name + " ,<br>" +
            "This your password : " + password +
            "<br>Thank You";
            message.Body = isipesan;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("web.tester1998@gmail.com", "cgv261479");
            client.EnableSsl = true;

            try
            {
                client.Send(message);
                //MessageBox.Show("Success Send Email");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Send Email" + ex.ToString());
            }
        }

        private void cb_role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                id_role = Convert.ToInt32(cb_role.SelectedValue.ToString());
            }
            catch (Exception)
            {

            }
        }
        
        //Validasi Email
        private Boolean email_validation(string email)
        {
            Boolean valid = true;

            int ambilsimbol = email.IndexOf("@");
            if(ambilsimbol == -1)
            {
                return false;
            }
            else
            {
                string emaildepan = email.Substring(0, ambilsimbol);
                string domainemail = email.Substring(ambilsimbol + 1);

                if (Regex.IsMatch(emaildepan, "[^a-zA-Z0-9._]"))
                {
                    valid = false;
                }

                if (domainemail == "gmail.com" || domainemail == "yahoo.com" || domainemail == "outlook.com")
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }

                return valid;
            }
        }

        //Insert & Update
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txb_id_supp.Text == "")
            {
                //Function Insert
                if (MessageBox.Show("Are you sure Insert this data?",
               "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (txb_name_supp.Text == "")
                    {
                        MessageBox.Show("Name cannot be empty");
                        txb_name_supp.Focus();
                    }
                    else if (txb_address_supp.Text == "")
                    {
                        MessageBox.Show("Address cannot be empty");
                        txb_address_supp.Focus();
                    }
                    else if (txb_email_supp.Text == "")
                    {
                        MessageBox.Show("Email cannot be empty");
                        txb_email_supp.Focus();
                    }
                    else if (email_validation(txb_email_supp.Text) == false)
                    {
                        MessageBox.Show("Format Email invalid");
                        txb_email_supp.Focus();
                    }
                    else if (cb_role.Text == "")
                    {
                        MessageBox.Show("Select a Role");
                        cb_role.Focus();
                    }
                    else
                    {
                        try
                        {
                            var email = con.Suppliers.Where(s => s.Email == txb_email_supp.Text).FirstOrDefault();
                            string password = System.Guid.NewGuid().ToString();
                            var role_id = con.Roles.Where(r => r.Id == id_role).FirstOrDefault();

                            String ket_role;
                            if (id_role == 1)
                            {
                                ket_role = "Admin";
                            }
                            else
                            {
                                ket_role = "User";
                            }

                            // new nama_model();
                            var input_supp = new Supplier(txb_name_supp.Text, txb_address_supp.Text, txb_email_supp.Text, password, ket_role, role_id);

                            Sendmail(txb_email_supp.Text, txb_name_supp.Text, password);

                            con.Suppliers.Add(input_supp);
                            con.SaveChanges();
                            tblsupplier.ItemsSource = con.Suppliers.ToList();
                            MessageBox.Show("Success Insert Data!");

                            empty_txb_supp();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Email Cannot Send Email or Email Registered");
                            empty_txb_supp();
                        }
                    }
                }
                else
                {
                    //do nothing
                }
            }
            else if (txb_id_supp.Text != null)
            {
                //Function Update
                if (MessageBox.Show("Are you sure Update this data?",
               "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (txb_name_supp.Text == "")
                    {
                        MessageBox.Show("Name cannot be empty");
                        txb_name_supp.Focus();
                    }
                    else if (txb_address_supp.Text == "")
                    {
                        MessageBox.Show("Address cannot be empty");
                        txb_address_supp.Focus();
                    }
                    else if (txb_address_supp.Text == "")
                    {
                        MessageBox.Show("Address cannot be empty");
                        txb_address_supp.Focus();
                    }
                    else
                    {
                        try
                        {
                            int id = Convert.ToInt32(txb_id_supp.Text); //mengambil id supplier
                            var role_id = con.Roles.Where(r => r.Id == id_role).FirstOrDefault(); //mengambil role id
                            var myid = con.Suppliers.Where(s => s.Id == id).FirstOrDefault(); // menginisiasi table supplier menjadi S

                            //Kolom yang diupdate
                            myid.Name = txb_name_supp.Text;
                            myid.Address = txb_address_supp.Text;
                            myid.Email = txb_email_supp.Text;
                            myid.Role = role_id;

                            con.SaveChanges();
                            tblsupplier.ItemsSource = con.Suppliers.ToList();
                            MessageBox.Show("Succes Update Data!");

                            empty_txb_supp();
                        }
                        catch (Exception)
                        {
                            //MessageBox.Show("Erro Update Data" + ex);
                        }
                    }
                }
                else
                {
                    //do nothing
                }
            }
        }

        //DataGrid klik
        private void tblsupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = tblsupplier.SelectedItem;
            if (data == null)
            {
                tblsupplier.Items.Refresh();
            }
            else
            {
                string id = (tblsupplier.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text; txb_id_supp.Text = id;
                string name = (tblsupplier.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text; txb_name_supp.Text = name;
                string address = (tblsupplier.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text; txb_address_supp.Text = address;
                string email = (tblsupplier.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text; txb_email_supp.Text = email;
                string role = (tblsupplier.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text; cb_role.Text = role;
            }
        }

        //Refresh
        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            empty_txb_supp();
            tblsupplier.ItemsSource = con.Suppliers.ToList();
        }

        //Hapus
        private void HapusRow(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure Delete this data?",
               "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    var data = tblsupplier.SelectedItem;
                    string id_datagrid = (tblsupplier.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                    int id = int.Parse(id_datagrid);
                    var myid = con.Suppliers.Where(s => s.Id == id).FirstOrDefault();

                    con.Suppliers.Remove(myid);
                    con.SaveChanges();
                    tblsupplier.ItemsSource = con.Suppliers.ToList();
                    MessageBox.Show("Succes Delete Data!");

                    empty_txb_supp();
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

        //Search
        private void Search_Btn(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Supplier> ListSupplier = new List<Supplier>();
                int intvalue;

                if (Search_box.Text == "")
                {
                    tblsupplier.ItemsSource = con.Suppliers.ToList();
                }

                foreach (Supplier supp in con.Suppliers.ToList())
                {
                    if (supp.Name.ToLower().Contains(Search_box.Text.ToLower()))
                    {
                        ListSupplier.Add(supp);
                    }
                    else if (int.TryParse(Search_box.Text, out intvalue))
                    {
                        if (supp.Id.Equals(Convert.ToInt32(Search_box.Text)))
                        {
                            ListSupplier.Add(supp);
                        }
                    }
                    else if (supp.Address.ToLower().Contains(Search_box.Text.ToLower()))
                    {
                        ListSupplier.Add(supp);
                    }
                    else if (supp.Email.ToLower().Contains(Search_box.Text.ToLower()))
                    {
                        ListSupplier.Add(supp);
                    }
                    else if (supp.Role.Name.ToLower().Contains(Search_box.Text.ToLower()))
                    {
                        ListSupplier.Add(supp);
                    }
                }

                tblsupplier.ItemsSource = ListSupplier.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        public void empty_txb_supp()
        {
            foreach (Control ct in container_supplier.Children)
            {
                try
                {
                    if (ct.GetType() == typeof(TextBox))
                    {
                        ((TextBox)ct).Text = String.Empty;
                    }
                    cb_role.SelectedIndex = -1;
                }
                catch (Exception)
                {
                    //do nothing
                }
            }
        }
    }
}
