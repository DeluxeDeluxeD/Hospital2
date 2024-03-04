using System.Windows.Controls;
using System.Windows;
using Hospital.Model;
using System.Linq;
using System;

namespace Hospital
{
    public partial class Authorization : Page
    {
        private Frame myFrame;

        public Authorization()
        {
            InitializeComponent();
            AppConnect.HospitalModel = new HospitalEntities9(); // подключение модели базы данных к странице авторизации
            AppFrame.frameMain = myFrame;
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            try
            {
                int cardNumber;
                if (int.TryParse(Txt_Card.Text, out cardNumber))
                {
                    var userObj = AppConnect.HospitalModel.Patients.FirstOrDefault(x => x.MedicalCardNumber == cardNumber);
                    if (userObj == null)
                    {
                        MessageBox.Show("Такого пациента не существует.");
                    }
                    else
                    {
                        VariableClass.Surname = userObj.Surname;
                        VariableClass.Name = userObj.Name;
                        VariableClass.Patronymic = userObj.Patronymic;
                        Manager.myFrame.Navigate(new PatientPersonalAccount());
                    }
                }
                else
                {
                    MessageBox.Show("Некорректный номер медицинской карты.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message + " Проверьте введенные данные и повторите попытку.");
            }
        }
    }
}
