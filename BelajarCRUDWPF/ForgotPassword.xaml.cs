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
using System.Windows.Shapes;

namespace BelajarCRUDWPF
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        myContext con = new myContext();
        Login login = new Login();

        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void Exit_Btn(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Back_Btn(object sender, MouseButtonEventArgs e)
        {
            login.Show();
            this.Close();
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

        private void forgotpass_btn_Click(object sender, RoutedEventArgs e)
        {
            if (txb_email.Text == "")
            {
                MessageBox.Show("Please fill Email");
                txb_email.Focus();
            }
            else
            {
                try
                {
                    string password = System.Guid.NewGuid().ToString();
                    var row = con.Suppliers.Where(r => r.Email == txb_email.Text).FirstOrDefault();
                    var myid = con.Suppliers.Where(s => s.Id == row.Id).FirstOrDefault();

                    Sendmail(txb_email.Text, row.Name, password);
                    myid.Password = password;
                    con.SaveChanges();

                    MessageBox.Show("Success Reset Password \nPlease Check Your Email");

                    login.Show();
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Email not Registered");
                    txb_email.Text = "";
                    txb_email.Focus();
                }
                
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
