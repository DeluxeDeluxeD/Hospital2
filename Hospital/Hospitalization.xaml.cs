using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows;
using Hospital.Model;
using System.Linq;

namespace Hospital
{
    public partial class Hospitalization : Page
    {
        private Frame myFrame;

        public Hospitalization()
        {
            InitializeComponent();
            AppConnect.HospitalModel = new HospitalEntities9();
            AppFrame.frameMain = myFrame;

            Patronymic.Text = VariableClass.Patronymic;
            Surname.Text = VariableClass.Surname;
            Seria.Text = VariableClass.Seria;
            Nomer.Text = VariableClass.Nomer;
            Polis.Text = VariableClass.Polis;
            Name.Text = VariableClass.Name;
            Work.Text = VariableClass.Work;
        }

        // Метод поиска Id пациента по фамилии
        private int GetPatientIdBySurname(string surname)
        {
            var patient = AppConnect.HospitalModel.Patients.FirstOrDefault(p => p.Surname == surname);
            return patient != null ? patient.Id : -1;
        }

        private void RecordButton(object sender, RoutedEventArgs e)
        {
            if (Surname.Text == "" || Name.Text == "" || Patronymic.Text == "" || Nomer.Text == "" || Seria.Text == "" || Work.Text == "" || Polis.Text == "")
            {
                MessageBox.Show("Заполните все поля.");
            }
            else
            {
                int patientId = GetPatientIdBySurname(Surname.Text);
                if (patientId != -1)
                {
                    HistoryHospitalizations history = new HistoryHospitalizations();
                    history.IdPatient = patientId;
                    history.DateHospitalization = Data_Hospitalization.SelectedDate.Value;

                    if (AppConnect.HospitalModel != null)
                    {
                        AppConnect.HospitalModel.HistoryHospitalizations.Add(history);
                        AppConnect.HospitalModel.SaveChanges();
                        MessageBox.Show("Дата госпитализации сохранена!");
                        NavigationService.Navigate(new PatientPersonalAccount());
                    }
                    else
                    {
                        MessageBox.Show("Ошибка в подключении к базе данных.");
                    }
                }                
            }
        }
    }
}
