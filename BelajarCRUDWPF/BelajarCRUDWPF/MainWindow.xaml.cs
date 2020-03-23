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
using System.Windows.Shapes;

namespace BelajarCRUDWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        myContext con = new myContext();
        public String Mail;

        public Window1(String Remail)
        {
            InitializeComponent();

            Mail = Remail;
            Role_Access();
        }
        private void Role_Access()
        {
            var role_akses = con.Suppliers.Where(S => S.Email == Mail).FirstOrDefault();
            if (role_akses.Ket_Role == "Admin")
            {
                //do nothing
            }
            else if (role_akses.Ket_Role == "User")
            {
                lvi_supplier.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Are you sure? \nYou will be Logout",
              "Confirmation Logout", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Login login = new Login();
                login.Show();

                this.Close();
            }
            else
            {

            }
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;

            switch (index)
            {
                case 0:
                    GridContainer.Children.Clear();
                    GridContainer.Children.Add(new UCTbItem());
                    break;
                case 1:
                    GridContainer.Children.Clear();
                    GridContainer.Children.Add(new UCTbSupplier());
                    break;
                case 2:
                    if (MessageBox.Show("Are you sure? \nYou will be Logout",
                    "Confirmation Logout", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Login login = new Login();
                        login.Show();

                        this.Close();
                    }
                    else
                    {
                    }
                    break;
                default:
                    break;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
