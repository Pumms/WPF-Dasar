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
using System.Windows.Shapes;

namespace BelajarCRUDWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        myContext con = new myContext();

        public Login()
        {
            InitializeComponent();
            txb_email.Focus();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Exit_Btn(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private Boolean email_validation(string email)
        {
            Boolean valid = true;

            int ambilsimbol = email.IndexOf("@");
            if (ambilsimbol == -1)
            {
                return false;
            }
            else
            {
                string emaildepan = email.Substring(0, ambilsimbol);
                string domainemail = email.Substring(ambilsimbol + 1);

                if (email == "@gmail.com" || email == "@yahoo.com")
                {
                    valid = false;
                }

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

        private void LoginUser()
        {
            try
            {
                if (txb_email.Text == "")
                {
                    MessageBox.Show("Please fill Email");
                    txb_email.Focus();
                }
                else if (email_validation(txb_email.Text) == false)
                {
                    MessageBox.Show("Format Email Invalid");
                    txb_email.Focus();
                }
                else if (txb_password.Password == "")
                {
                    MessageBox.Show("Please fill Password");
                    txb_password.Focus();
                }
                else
                {
                    var row = con.Suppliers.Where(s => s.Email == txb_email.Text).FirstOrDefault();

                    if (row.Password != txb_password.Password)
                    {
                        MessageBox.Show("Incorrect Password!");
                        txb_password.Password = "";
                        txb_password.Focus();
                    }
                    else if (row.Email == txb_email.Text)
                    {
                        Window1 w1 = new Window1(txb_email.Text);
                        w1.Show();
                        this.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Incorrect Email or Password!");
            }
            
        }

        private void BtnForgot_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword fp = new ForgotPassword();
            fp.Show();
            this.Close();
        }

        public void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginUser();
        }

        private void txb_password_TouchEnter(object sender, TouchEventArgs e)
        {
            LoginUser();
        }

        private void txb_email_TouchEnter(object sender, TouchEventArgs e)
        {
            LoginUser();
        }

        private void txb_password_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
