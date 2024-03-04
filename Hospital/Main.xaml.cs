using System.Windows.Controls;
using System.Windows;

namespace Hospital
{
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }

        public void LoginButton(object sender, RoutedEventArgs e)
        {
            Manager.myFrame.Navigate(new Authorization());
        }

        public void RegisterButton(object sender, RoutedEventArgs e)
        {
            Manager.myFrame.Navigate(new Registration());
        }

        public void LoginDoctorButton(object sender, RoutedEventArgs e)
        {
            Manager.myFrame.Navigate(new AuthorizationDoctor());
        }
    }
}
