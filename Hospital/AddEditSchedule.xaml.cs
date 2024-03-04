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

namespace Hospital
{
    public partial class AddEdit : Page
    {
        private ReceptionSchedule _currentReceptionSchedule = new ReceptionSchedule();

        public AddEdit()
        {
            InitializeComponent();
            DataContext = _currentReceptionSchedule;
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (errors.Length > 0)
                MessageBox.Show(errors.ToString());
        }
    }
}
