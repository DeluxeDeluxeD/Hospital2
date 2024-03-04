using System.Windows.Controls;
using System.Windows;

namespace Hospital
{
    public partial class DoctorPersonalAccount : Page
    {
        public DoctorPersonalAccount()
        {
            InitializeComponent();
            Surname.Text = VariableClass.Surname;
            Name.Text = VariableClass.Name;
            Patronymic.Text = VariableClass.Patronymic;
        }

        private void HospitalizationButton(object sender, RoutedEventArgs e)
        {
            Manager.myFrame.Navigate(new MedicalDiagnosticProcedure());
        }

        private void ScheduleButton(object sender, RoutedEventArgs e)
        {
            Manager.myFrame.Navigate(new ReceptionSchedule());
        }

        public void ExitButton(object sender, RoutedEventArgs e)
        {
            Manager.myFrame.Navigate(new Main());
        }
    }
}
